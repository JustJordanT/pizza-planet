using Microsoft.AspNetCore.Mvc;
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
    private readonly ICustomerRepository _customerRepository;
    private readonly ICartRepository _cartRepository;

    public PizzasRepository(
        PgSqlContext pgSqlContext,
        ICustomerRepository customerRepository,
        ICartRepository cartRepository)
    {
        _pgSqlContext = pgSqlContext ?? throw new ArgumentNullException(nameof(pgSqlContext));
        _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
        _cartRepository = cartRepository ?? throw new ArgumentNullException(nameof(cartRepository));
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
    public async Task CreatePizzasAsync(CreatePizzaModel createPizzaModel, string email, CancellationToken cancellationToken)
    {
        var customer = await _customerRepository
            .GetCustomerByEmailAsync(email, cancellationToken);
        var cart = await _cartRepository
            .GetCartFromCustomerId(customer.Id, cancellationToken);
        
        // await _cartRepository.UpdateCartAsync(pizzaId);


        await _pgSqlContext.PizzasEntity.AddAsync(Mappers.CreatePizzaModelToPizzasEntity(createPizzaModel, cart.Id), cancellationToken);
        // cart.PizzaId = await GetPizzaByCartIdAsync(cart.Id, cancellationToken);
        // var pizzas = await GetPizzasFromCartAsync(email, cancellationToken);
        
        // await _cartRepository.UpdatePrice(customer.Id ,pizzas.Sum(s => s.Price), cancellationToken);
        // await _cartRepository.UpdateQuantity(customer.Id, pizzas.Sum(s => s.Quantity), cancellationToken);
        await _pgSqlContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<List<PizzasEntity>> GetPizzasFromCartAsync(string email, CancellationToken cancellationToken)
    {
        var customer = await _customerRepository
            .GetCustomerByEmailAsync(email, cancellationToken);
        var cart = await _cartRepository
            .GetCartFromCustomerId(customer.Id, cancellationToken);

        return await _pgSqlContext.PizzasEntity.Where(c => c.CartId == cart.Id).ToListAsync(cancellationToken);
    }

    // public async Task<string> GetPizzaByCartIdAsync(string cartId, CancellationToken cancellationToken)
    // {
    //     var cart = await _cartRepository
    //         .GetCartFromCustomerId(customer.Id, cancellationToken);
    //     var thing = await _pgSqlContext.PizzasEntity
    //         .FirstOrDefaultAsync(pizza => pizza.CartId == cartId, cancellationToken: cancellationToken);
    //     return thing.CartId;
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