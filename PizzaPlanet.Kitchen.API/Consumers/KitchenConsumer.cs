using MassTransit;
using PizzaPlanet.Messages;
using PizzaPlanet.Messages;

namespace PizzaPlanet.Kitchen.API.Consumers
{
    public class KitchenConsumer : IConsumer<IPublishOrder>
    {
        private readonly ILogger _logger;

        public KitchenConsumer(ILogger logger)
        {
            _logger = logger;
        }

        // public KitchenConsumer()
        // {
        //     
        // }

        public Task Consume(ConsumeContext<IPublishOrder> context)
        {
            _logger.LogInformation("Order received with an id of: {Id} " + context.Message.Id);
            return Task.CompletedTask;
        }
    }
}