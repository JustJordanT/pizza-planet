using PizzaPlanet.API.Entities;
using PizzaPlanet.API.Models;

namespace PizzaPlanet.API.Commons;

public class CartMapper
{
    public static CartEntity CreateCartModelToCartEntity(CreateCartModel createCartModel, decimal pizzaPriceTotal, int pizzaQuantity)
    {
    
       var pizzaIds = createCartModel.PizzasEntities.ToList();
    
       var passedCart = new CartEntity
       {
          CustomerId = createCartModel.CustomerId,
          Price = pizzaPriceTotal,
          Quantity = pizzaQuantity
       };
       
       return passedCart;
    }

    public static CartEntity InitCartModelToCartEntity(string initCart)
    {
        var passedCart = new CartEntity
        {
            CustomerId = initCart
        };
        
        return passedCart;
    }
    
}