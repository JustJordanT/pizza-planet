using MassTransit;
using PizzaPlanet.Kitchen.API.Contracts;

namespace PizzaPlanet.Kitchen.API.Consumers
{
    public class KitchenConsumer :
        IConsumer<OrderConsumer>
    {
        public Task Consume(ConsumeContext<OrderConsumer> context)
        {
            return Task.CompletedTask;
        }
    }
}