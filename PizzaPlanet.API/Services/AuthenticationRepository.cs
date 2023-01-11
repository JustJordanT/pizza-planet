using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;
using PizzaPlanet.API.Models;

namespace PizzaPlanet.API.Services.Interfaces;

public class AuthenticationRepository : IAuthenticationRepository
{
    private readonly IConfiguration _configuration;

    public AuthenticationRepository(IConfiguration configuration)
    {
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
    }
    
    public void CreatePasswordCrypt(string password, out string passwordCrypt, out string passwordHash, out string passwordSalt)
    {
        passwordHash = BCrypt.Net.BCrypt.HashPassword(password);
        passwordSalt = BCrypt.Net.BCrypt.GenerateSalt(10);
        passwordCrypt = BCrypt.Net.BCrypt.HashPassword(password, passwordSalt);
    }

    public async Task<bool> VerifyPassword(string password, string passwordHashSalt)
    {
       return BCrypt.Net.BCrypt.Verify(password, passwordHashSalt);
    }

    public string GenerateJwtToken(LoginCustomer customer)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Email, customer.Email)
        };

        var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);
        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddDays(7),
            signingCredentials: credentials);
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}