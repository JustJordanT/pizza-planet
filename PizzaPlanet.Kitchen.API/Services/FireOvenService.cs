using PizzaPlanet.Kitchen.API.Commons;
using PizzaPlanet.Kitchen.API.Entities;
using PizzaPlanet.Kitchen.API.Services.Interfaces;

namespace PizzaPlanet.Kitchen.API.Services;

public class FireOvenService : IFireOven
{
    private readonly ICooksRepository _cooksRepository;
    private readonly IPizzasCompletedRepository _pizzasCompletedRepository;
    private readonly ICommonsRepository _commonsRepository;

    public FireOvenService(ICooksRepository cooksRepository, IPizzasCompletedRepository pizzasCompletedRepository, ICommonsRepository commonsRepository)
    {
        _cooksRepository = cooksRepository ?? throw new ArgumentNullException(nameof(cooksRepository));
        _pizzasCompletedRepository = pizzasCompletedRepository ?? throw new ArgumentNullException(nameof(pizzasCompletedRepository));
        _commonsRepository = commonsRepository ?? throw new ArgumentNullException(nameof(commonsRepository));
    }
    
    public async Task CookPizza(string pizzaId, OrderStatus orderStatus)
    {
        //TODO Send OrderStatus back to PizzaShop


        await Task.Delay(20000);
        // TODO Add a entry to pizza completed table for pizza that has been completed
        await _pizzasCompletedRepository.PizzasCompleted(pizzaId, await _cooksRepository.GetRandomCookId());
        await _commonsRepository.SaveChangesAsync(new CancellationToken());
    }
}