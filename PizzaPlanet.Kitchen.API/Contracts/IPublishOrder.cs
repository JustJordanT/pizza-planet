using MassTransit;

namespace PizzaPlanet.Messages;

public class IPublishOrder : IConsumer
{
    public string Id { get; set; }
    public List<string> PizzaIds { get; set;}
}