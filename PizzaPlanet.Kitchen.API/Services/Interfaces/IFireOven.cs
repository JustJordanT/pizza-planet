using PizzaPlanet.Kitchen.API.Commons;
using PizzaPlanet.Kitchen.API.Entities;

namespace PizzaPlanet.Kitchen.API.Services.Interfaces;

public interface IFireOven
{
   Task CookPizza(string pizzaId, OrderStatus updateStatus);
}