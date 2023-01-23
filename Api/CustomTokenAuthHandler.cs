using System.Security.Claims;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;

namespace Api;

public class CustomTokenAuthHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
    private readonly ITokenValidationService _tokenService;
    public CustomTokenAuthHandler(IOptionsMonitor<AuthenticationSchemeOptions> options,
                                      ILoggerFactory logger,
                                      UrlEncoder encoder,
                                      ISystemClock clock,
                                      ITokenValidationService tokenService) : base(options, logger, encoder, clock)
    {
        _tokenService = tokenService;
    }

    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        if (!Request.Headers.ContainsKey(HeaderNames.Authorization))
        {
            return await Task.FromResult(AuthenticateResult.Fail("Header Not Found."));
        }

        var authorizationHeader = Request.Headers["Authorization"].ToString();
        var encodedToken = authorizationHeader.Substring("Basic ".Length).Trim();
        var user = await _tokenService.Validate(encodedToken);
        if(user != null)
        {
            var claims = new[] { new Claim("userid", user.UserId.ToString()) };
            var identity = new ClaimsIdentity(claims, "Basic");
            var claimsPrincipal = new ClaimsPrincipal(identity);
            return await Task.FromResult(AuthenticateResult.Success(new AuthenticationTicket(claimsPrincipal, Scheme.Name)));
        }

        Response.StatusCode = 401;
        return await Task.FromResult(AuthenticateResult.Fail("Invalid Authorization Header"));
    }
}