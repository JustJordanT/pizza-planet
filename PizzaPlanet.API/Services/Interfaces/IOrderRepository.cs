using PizzaPlanet.API.Models;

namespace PizzaPlanet.API.Services.Interfaces;

public interface IOrderRepository
{
    Task InitCustomerOrder(CreateCustomer customer, CancellationToken cancellationToken); 
}