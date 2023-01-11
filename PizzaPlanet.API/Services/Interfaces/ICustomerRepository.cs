using PizzaPlanet.API.Entities;
using PizzaPlanet.API.Models;

namespace PizzaPlanet.API.Services.Interfaces;

public interface ICustomerRepository
{ 
    Task CreateCustomerAsync(CreateCustomer customer, CancellationToken cancellationToken); 
    Task<bool> GetCustomerByEmail(string email);
    Task<bool> VerifyCustomerPassword(string email, string password);
    string CreateToken(LoginCustomer customer);
}