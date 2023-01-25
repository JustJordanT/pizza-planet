using PizzaPlanet.Kitchen.API.Entities;
using PizzaPlanet.Kitchen.API.Models;

namespace PizzaPlanet.Kitchen.API.Commons.Mapper;

public static class OrderMapper
{
    public static Orders_Completed CreateOrdersCompleted(CreateOrderCompleted createOrderCompleted)
    {
        var order = new Orders_Completed()
        {
            OrderId = createOrderCompleted.OrderId,
            CookId = createOrderCompleted.CookId,
            OrderStatus = nameof(OrderStatus.OrderCompleted)
        };
        
        return order;
    }
}