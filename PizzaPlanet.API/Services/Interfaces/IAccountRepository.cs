using PizzaPlanet.API.Entities;
using PizzaPlanet.API.Models;

namespace PizzaPlanet.API.Services;

public interface IAccountRepository
{
    Task<CartEntity> GetCartFromCustomerId(string email, CancellationToken cancellationToken);
    Task<IEnumerable<string>> GetAllCartsFromCustomerId(string email, CancellationToken cancellationToken);
    Task<CustomerEntity> GetCustomerByEmailAsync(string email, CancellationToken cancellationToken);
    Task<OrderEntity?> GetOrderFromCartId(CartEntity cart, string email, CancellationToken cancellationToken);
    Task<List<OrderEntity?>> GetAllOrdersFromCartId(IEnumerable<string> cartIdList, string email,
        CancellationToken cancellationToken);
}