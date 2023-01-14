using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Bson;
using MongoDB.Driver;
using PizzaPlanet.API.Commons;
using PizzaPlanet.API.Context;
using PizzaPlanet.API.Entities;
using PizzaPlanet.API.Models;
using PizzaPlanet.API.Services.Interfaces;

namespace PizzaPlanet.API.Services;

public class PizzasRepository : IPizzaRepository
{
    private readonly PgSqlContext _pgSqlContext;

    public PizzasRepository(PgSqlContext pgSqlContext)
    {
        _pgSqlContext = pgSqlContext ?? throw new ArgumentNullException(nameof(pgSqlContext));
    }
    
    // public async Task<List<PizzasEntity>> GetAllPizzasAsync(CancellationToken cancellationToken)
    // {
    //     // return await MongoCollection.Find(_ => true).ToListAsync(cancellationToken: cancellationToken);
    //     return await _pgSqlContext.PizzasEntity.ToListAsync(cancellationToken);
    // }
    //
    // public async Task<PizzasEntity?> GetPizzasByIdAsync(string id, CancellationToken cancellationToken)
    // {
    //     // var idCheck = await MongoCollection.Find(_ => _.Id == id).AnyAsync();
    //      var idCheck = await _pgSqlContext
    //          .PizzasEntity
    //          .AnyAsync(c => c.Id == id, cancellationToken: cancellationToken);
    //
    //      if (!idCheck)
    //      { 
    //          return null;
    //      }
    //
    //      return await _pgSqlContext.PizzasEntity
    //          .Where(p => p.Id == id)
    //          .FirstOrDefaultAsync(cancellationToken: cancellationToken);
    //
    //      // if (!idCheck) return null;
    //      // var pizza = MongoCollection.Find(p => p.Id == id);
    //      // return pizza;
    // }
    //
    // public async Task<decimal> GetPizzaPrice(List<string> ids, CancellationToken cancellationToken)
    // {
    //     var total = 0m;
    //
    //     foreach (var pizzaId in ids)
    //     {
    //         var price = await GetPizzasByIdAsync(pizzaId, new CancellationToken());
    //         total += price.Price;
    //     }
    //     return total;
    // }
    //
    // public async Task<int> GetPizzaQuantity(List<string> ids, CancellationToken cancellationToken)
    // {
    //     var total = 0;
    //
    //     foreach (var pizzaId in ids)
    //     {
    //         var quantity = await GetPizzasByIdAsync(pizzaId, new CancellationToken());
    //         total += quantity.Quantity;
    //     }
    //     return total;
    // }
    //
    // public async Task CreatePizzasAsync(CreatePizzaModel createPizzaModel,CancellationToken cancellationToken)
    // {
    //     await _pgSqlContext.PizzasEntity.AddAsync(Mappers.CreatePizzaModelToPizzasEntity(createPizzaModel), cancellationToken);
    //     await _pgSqlContext.SaveChangesAsync(cancellationToken);
    // }
    //
    // public async Task PutPizzasAsync(string id ,PutPizzaModel putPizzaModel, CancellationToken cancellationToken)
    // {
    //     var pizza = await GetPizzasByIdAsync(id, new CancellationToken());
    //     Mappers.PizzasEntityToPutPizzaModel(pizza, putPizzaModel);
    //     await _pgSqlContext.SaveChangesAsync(cancellationToken);
    // }
    //
    // // TODO something is wrong with the delete here not able to delete any records.
    // public async Task DeletePizzasAsync(string id, CancellationToken cancellationToken)
    // {
    //     var item = await _pgSqlContext.PizzasEntity
    //         .SingleOrDefaultAsync(p => p.Id == id, cancellationToken);
    //     if (item != null)
    //     {
    //         _pgSqlContext.PizzasEntity.Remove(item);
    //         await _pgSqlContext.SaveChangesAsync(cancellationToken);
    //     }
    // }
}