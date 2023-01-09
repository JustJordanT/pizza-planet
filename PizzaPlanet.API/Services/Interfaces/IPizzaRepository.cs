using MongoDB.Bson;
using MongoDB.Driver;
using PizzaPlanet.API.Entities;
using PizzaPlanet.API.Models;

namespace PizzaPlanet.API.Services.Interfaces;

public interface IPizzaRepository
{
    Task<List<PizzasEntity>> GetAllPizzasAsync(CancellationToken cancellationToken);
    Task<List<PizzasEntity>> GetPizzasByIdAsync(string id);

    Task CreatePizzaAsync(PizzaModel pizzaModel, CancellationToken cancellationToken);
    
    Task UpdatePizzaAsync(string id ,PutPizzaModel putPizzaModel, CancellationToken cancellationToken);
    
    Task<object> DeletePizzaAsync(string id, CancellationToken cancellationToken);
}