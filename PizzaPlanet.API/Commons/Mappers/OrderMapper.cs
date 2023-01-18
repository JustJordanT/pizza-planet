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
}