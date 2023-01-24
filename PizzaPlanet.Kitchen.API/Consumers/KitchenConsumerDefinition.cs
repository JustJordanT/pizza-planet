namespace Company.Consumers
{
    using MassTransit;

    public class Piz :
        ConsumerDefinition<OrderC>
    {
        protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator, IConsumerConfigurator<PizzaPlanet.Kitchen.APIConsumer> consumerConfigurator)
        {
            endpointConfigurator.UseMessageRetry(r => r.Intervals(500, 1000));
        }
    }
}