using MassTransit;
using PizzaPlanet.Kitchen.API.Commons;
using PizzaPlanet.Kitchen.API.Services.Interfaces;
using PizzaPlanet.Messages;
using PizzaPlanet.Messages;

namespace PizzaPlanet.Kitchen.API.Consumers
{
    public class KitchenConsumer : IConsumer<IPublishOrder>
    {
        private readonly ILogger _logger;
        private readonly IFireOven _fireOven;

        public KitchenConsumer(ILogger logger, IFireOven fireOven)
        {
            _logger = logger;
            _fireOven = fireOven;
        }

        // public KitchenConsumer(ILogger logger)
        // {
        //     _logger = logger;
        // }

        public async Task Consume(ConsumeContext<IPublishOrder> context)
        {
            _logger.LogInformation("Order received with an id of: {Id} " + context.Message.Id);
            foreach (var pizzaId in context.Message.PizzaIds)
            {
              _logger.LogInformation("Pizza Id: {Id} " + pizzaId);
              await _fireOven.CookPizza(pizzaId, OrderStatus.OrderConfirmed);
            }
            return Task.CompletedTask;
        }
    }
}