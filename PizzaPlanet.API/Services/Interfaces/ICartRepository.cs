using PizzaPlanet.API.Entities;
using PizzaPlanet.API.Models;

namespace PizzaPlanet.API.Services.Interfaces;

public interface ICartRepository
{
    Task InitCustomerCart(CreateCustomer customer, CancellationToken cancellationToken);
    Task GetCartFromCustomerId(string customerId, CancellationToken cancellationToken);
    Task CreateCartAsync(string initCart, CancellationToken cancellationToken);
    // Task<List<CartEntity>> GetCartsAsync(CancellationToken cancellationToken);
    // Task<List<CartEntity>> GetCartsByIdAsync(string id, CancellationToken cancellationToken);
    // Task PutCartsAsync(PutCartModel putCartModel,string id, List<string> pizzaIds, CancellationToken cancellationToken);
    //
    //
}