using System.Text;
using Api.Data;
using Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Api;

public class TokenValidationService : ITokenValidationService
{
    private readonly DbSet<User> _users;

    public TokenValidationService(ApiDbContext context)
    {
        _users = context.Users;
    }

    public async Task<User?> Validate(string encodedString)
    {
        var base64EncodedBytes = System.Convert.FromBase64String(encodedString);
        var token = Encoding.UTF8.GetString(base64EncodedBytes);
        var user = await _users.FirstOrDefaultAsync(u => u.UserToken == token);
        if(user == null)
        {
            return null;
        }
        return user;
    }
}
