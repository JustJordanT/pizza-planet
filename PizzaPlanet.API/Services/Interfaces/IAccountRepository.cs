using PizzaPlanet.API.Entities;

namespace PizzaPlanet.API.Services;

public interface IAccountRepository
{
    Task<CartEntity> GetCartFromCustomerId(string email, CancellationToken cancellationToken);
    Task<CustomerEntity> GetCustomerByEmailAsync(string email, CancellationToken cancellationToken);
}