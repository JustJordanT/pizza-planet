using PizzaPlanet.API.Models;

namespace PizzaPlanet.API;

public class OrdersDataStore
{
    public List<Pizzas> Orders { get; set; }
    public static OrdersDataStore Current { get; } = new OrdersDataStore();

    private OrdersDataStore()
    {
        Orders = new List<Pizzas>()
        {
            new Pizzas()
            {
                Id = 1,
                Name = "Nova",
                Crust = "Hand Tossed",
                Toppings = new List<string> { "Pep", "Onion", "Garlic" },
                Size = "Large",
                CreatedAt = DateTime.UtcNow
            },
            
            new Pizzas()
            {
                Id = 2,
                Name = "Amaiah",
                Crust = "Hand Tossed",
                Toppings = new List<string> { "Pep" },
                Size = "Medium",
                CreatedAt = DateTime.UtcNow
            }
        };
    }


}