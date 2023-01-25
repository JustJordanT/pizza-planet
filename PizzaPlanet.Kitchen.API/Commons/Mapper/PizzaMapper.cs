using PizzaPlanet.Kitchen.API.Entities;
using PizzaPlanet.Kitchen.API.Models;

namespace PizzaPlanet.Kitchen.API.Commons.Mapper;

public static class PizzaMapper
{
    public static Pizzas_Completed CreatePizzasCompleted(CreatePizzasCompleted createPizzasCompleted)
    {
        var pizzaCompleted = new Pizzas_Completed
        {
            PizzaId = createPizzasCompleted.PizzaId,
            CookId = createPizzasCompleted.CookId,
        };

        return pizzaCompleted;
    }
}