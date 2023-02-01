using MassTransit;

namespace PizzaPlanet.Contracts;

public class IPublishOrderStatus : IConsumer
{
    public string Id { get; set; }
    public string OrderStatus { get; set;}
}