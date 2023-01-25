using MassTransit;

namespace PizzaPlanet.Contracts;

public class IPublishOrder : IConsumer
{
    public string Id { get; set; }
    public List<string> PizzaIds { get; set;}
}