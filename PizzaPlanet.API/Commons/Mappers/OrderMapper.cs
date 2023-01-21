using PizzaPlanet.API.Entities;
using PizzaPlanet.API.Models;

namespace PizzaPlanet.API.Commons;

public static class OrderMapper
{
    public static OrderEntity InitOrder(string cartId)
    {
        var passedCart = new OrderEntity
        {
            CartId = cartId
        };
        
        return passedCart;
    }
    
    public static List<GetOrders> ListOfOrders(IEnumerable<OrderEntity> orderEntity)
    {
        return orderEntity.Select(pizza => new GetOrders()
            {
                Id = pizza.Id,
                OrderStatus = pizza.OrderStatus
            })
            .ToList();
    }

    public static PublishOrder PublishOrderId(OrderEntity orderEntity)
    {
        var order = new PublishOrder
        {
            Id = orderEntity.Id
        };
        return order;
    }
}