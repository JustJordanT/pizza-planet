using PizzaPlanet.API.Models;

namespace PizzaPlanet.API.Services.Interfaces;

public interface IAuthenticationRepository
{
    void CreatePasswordCrypt(string password, out string passwordCrypt, out string passwordHash, out string passwordSalt);
    Task<bool> VerifyPassword(string password, string passwordHash);
    string GenerateJwtToken(LoginCustomer customer);
}