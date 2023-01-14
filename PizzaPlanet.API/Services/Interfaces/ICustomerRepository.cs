using MongoDB.Driver;
using PizzaPlanet.API.Entities;
using PizzaPlanet.API.Models;

namespace PizzaPlanet.API.Services.Interfaces;

public interface ICustomerRepository
{ 
    Task CreateCustomerAsync(CreateCustomer customer, CancellationToken cancellationToken);

    // Task<bool> CheckIfEmailAlreadyExistsAsync(, CancellationToken cancellationToken);
    // Task<bool> GetCustomerByEmail(string email);
    // Task<bool> VerifyCustomerPassword(string email, string password);
    // string CreateToken(LoginCustomer customer);
    //
    // Task<List<CustomerEntity>> GetCustomersAsync(CancellationToken cancellationToken);
    // Task<CustomerEntity> GetCustomersByIdAsync(string id, CancellationToken cancellationToken);
    // Task<CustomerEntity> PutCustomersAsync(string id, PutCustomerModel putCustomerModel,
    //     CancellationToken cancellationToken);
    //
    // Task<object> DeleteCustomersAsync(string id, CancellationToken cancellationToken);
}