namespace PizzaPlanet.Kitchen.API.Services.Interfaces;

public interface IOrdersCompletedRepository
{
    Task OrderCompleted(string oderId, string cookId);
}