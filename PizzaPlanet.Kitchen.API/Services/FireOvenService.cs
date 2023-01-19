using PizzaPlanet.Kitchen.API.Commons;
using PizzaPlanet.Kitchen.API.Entities;
using PizzaPlanet.Kitchen.API.Services.Interfaces;

namespace PizzaPlanet.Kitchen.API.Services;

public class FireOvenService : IFireOven
{
    public Task<Pizzas_Completed> CookPizza(string pizzaId, OrderStatus updateStatus)
    {
        throw new NotImplementedException();
    }
}