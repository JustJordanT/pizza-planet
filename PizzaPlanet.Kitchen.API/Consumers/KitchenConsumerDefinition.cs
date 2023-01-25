using MassTransit;

namespace PizzaPlanet.Kitchen.API.Consumers
{
    public class KitchenConsumerDefinition :
        ConsumerDefinition<KitchenConsumer>
    {
        protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator, IConsumerConfigurator<KitchenConsumer> consumerConfigurator)
        {
            endpointConfigurator.UseMessageRetry(r => r.Intervals(500, 1000));
        }
    }
}