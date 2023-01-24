using MassTransit;

namespace PizzaPlanet.Messages;

public class OrderConsumer : IConsumer
{
    public string Id { get; set; }
}