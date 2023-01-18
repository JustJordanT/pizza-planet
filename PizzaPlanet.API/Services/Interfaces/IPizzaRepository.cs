using MongoDB.Bson;
using MongoDB.Driver;
using PizzaPlanet.API.Entities;
using PizzaPlanet.API.Models;

namespace PizzaPlanet.API.Services.Interfaces;

public interface IPizzaRepository
{
    Task<bool> PizzaExistsAsync(string id);
    Task<PizzasEntity> GetPizzasByIdAsync(string id, CancellationToken cancellationToken);
    Task CreatePizzasAsync(CreatePizzaModel createPizzaModel, string email, CancellationToken cancellationToken);
    
    Task<List<PizzasEntity>> GetPizzasFromCartAsync(string email, CancellationToken cancellationToken);    //
    Task PutPizzasAsync(string id ,PutPizzaModel putPizzaModel, CancellationToken cancellationToken);
    Task DeletePizzasAsync(PizzasEntity pizza, CancellationToken cancellationToken);
}