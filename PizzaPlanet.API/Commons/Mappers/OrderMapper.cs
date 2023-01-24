using PizzaPlanet.API.Entities;
using PizzaPlanet.API.Models;
using PizzaPlanet.Messages;

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

    public static IPublishOrder PublishOrderId(OrderEntity orderEntity)
    {
        var order = new IPublishOrder
        {
            Id = orderEntity.Id
        };
        return order;
    }
}