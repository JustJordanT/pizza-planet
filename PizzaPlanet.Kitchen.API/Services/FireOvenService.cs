using PizzaPlanet.Kitchen.API.Commons;
using PizzaPlanet.Kitchen.API.Entities;
using PizzaPlanet.Kitchen.API.Services.Interfaces;

namespace PizzaPlanet.Kitchen.API.Services;

public class FireOvenService : IFireOven
{
    private readonly ICooksRepository _cooksRepository;
    private readonly IPizzasCompletedRepository _pizzasCompletedRepository;
    private readonly ICommonsRepository _commonsRepository;
    private readonly IOrdersCompletedRepository _ordersCompletedRepository;
    private string cookId;
    public FireOvenService(
        ICooksRepository cooksRepository,
        IPizzasCompletedRepository pizzasCompletedRepository,
        ICommonsRepository commonsRepository,
        IOrdersCompletedRepository ordersCompletedRepository)
    {
        _cooksRepository = cooksRepository ?? throw new ArgumentNullException(nameof(cooksRepository));
        _pizzasCompletedRepository = pizzasCompletedRepository ?? throw new ArgumentNullException(nameof(pizzasCompletedRepository));
        _commonsRepository = commonsRepository ?? throw new ArgumentNullException(nameof(commonsRepository));
        _ordersCompletedRepository = ordersCompletedRepository ?? throw new ArgumentNullException(nameof(ordersCompletedRepository));
    }
    
    public async Task CookPizza(string pizzaId, OrderStatus orderStatus)
    {
        //TODO Send OrderStatus back to PizzaShop


        await Task.Delay(20000);
        var cook = await _cooksRepository.GetRandomCookId();
        cookId = cook;
        await _pizzasCompletedRepository.PizzasCompleted(pizzaId, cook);
        await _commonsRepository.SaveChangesAsync(new CancellationToken());
    }

    public async Task CompleteOrder(string orderId)
    {
        // TODO Send OrderStatus back to PizzaShop
        await Task.Delay(10000);
        await _ordersCompletedRepository.OrderCompleted(orderId, cookId);
        await _commonsRepository.SaveChangesAsync(new CancellationToken());

    }
}