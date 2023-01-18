using PizzaPlanet.API.Commons;
using PizzaPlanet.API.Entities;
using PizzaPlanet.API.Models;

namespace PizzaPlanet.API.Services.Interfaces;

public interface IOrderRepository
{
    Task InitCustomerOrder(CreateCustomer customer, CancellationToken cancellationToken);
    Task UpdateOrderStatus(CartEntity cart,string email, CancellationToken cancellationToken);
    Task ResetOrderAsync(CartEntity cart, string email, CancellationToken cancellationToken);
}