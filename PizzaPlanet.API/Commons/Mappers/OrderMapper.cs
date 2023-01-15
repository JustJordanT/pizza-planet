using PizzaPlanet.API.Entities;
using PizzaPlanet.API.Models;

namespace PizzaPlanet.API.Commons;

public class OrderMapper
{
    public static OrderEntity InitOrder(string cartId)
    {
        var passedCart = new OrderEntity
        {
            CartId = cartId
        };
        
        return passedCart;
    }
}