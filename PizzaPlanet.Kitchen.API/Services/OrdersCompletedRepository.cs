using System.Collections.Specialized;
using PizzaPlanet.Kitchen.API.Commons;
using PizzaPlanet.Kitchen.API.Commons.Mapper;
using PizzaPlanet.Kitchen.API.Context;
using PizzaPlanet.Kitchen.API.Models;

namespace PizzaPlanet.Kitchen.API.Services.Interfaces;

public class OrdersCompletedRepository : IOrdersCompletedRepository
{
    private readonly KitchenDbContext _kitchenDbContext;

    public OrdersCompletedRepository(KitchenDbContext kitchenDbContext)
    {
        _kitchenDbContext = kitchenDbContext;
    }
    
    public async Task OrderCompleted(string oderId, string cookId)
    {
        var order = new CreateOrderCompleted()
        {
            OrderId = oderId,
            CookId = cookId,
        };
      
        await _kitchenDbContext.OrdersCompleted.AddAsync(OrderMapper.CreateOrdersCompleted(order));
      
    } 
}