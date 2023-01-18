using PizzaPlanet.API.Entities;
using PizzaPlanet.API.Models;

namespace PizzaPlanet.API.Services;

public interface IAccountRepository
{
    Task<CartEntity> GetCartFromCustomerId(string email, CancellationToken cancellationToken);
    Task<CustomerEntity> GetCustomerByEmailAsync(string email, CancellationToken cancellationToken);
    Task<OrderEntity> GetOrderFromCartId(string email, CancellationToken cancellationToken);
}