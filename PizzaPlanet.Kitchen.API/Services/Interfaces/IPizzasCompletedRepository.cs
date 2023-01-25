namespace PizzaPlanet.Kitchen.API.Services.Interfaces;

public interface IPizzasCompletedRepository
{
   Task PizzasCompleted(string pizzaId, string cookId);
}