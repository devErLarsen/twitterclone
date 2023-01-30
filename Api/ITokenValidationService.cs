using Api.Entities;

namespace Api;
public interface ITokenValidationService
{
    Task<User?> Validate(string encodedString);
}