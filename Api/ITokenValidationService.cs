using Api.Models;

namespace Api;

public interface ITokenValidationService
{
    Task<User?> Validate(string encodedString);
}