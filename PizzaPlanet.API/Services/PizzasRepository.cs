using Microsoft.EntityFrameworkCore;
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
    private readonly IAccountRepository _accountRepository;

    public PizzasRepository(
        PgSqlContext pgSqlContext,
        ICustomerRepository customerRepository,
        ICartRepository cartRepository,
        IAccountRepository acountRepository)
    {
        _pgSqlContext = pgSqlContext ?? throw new ArgumentNullException(nameof(pgSqlContext));
        _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
        _cartRepository = cartRepository ?? throw new ArgumentNullException(nameof(cartRepository));
        _accountRepository = acountRepository ?? throw new ArgumentNullException(nameof(acountRepository));
    }


    public async Task<bool> PizzaExistsAsync(string id)
    {
        return await _pgSqlContext.PizzasEntity.AnyAsync(pizza => pizza.Id == id);
    } 

    public async Task<PizzasEntity> GetPizzasByIdAsync(string id, CancellationToken cancellationToken)
    {
        // var idCheck = await MongoCollection.Find(_ => _.Id == id).AnyAsync();
         var idCheck = await _pgSqlContext
             .PizzasEntity
             .AnyAsync(c => c.Id == id, cancellationToken: cancellationToken);
    
         if (!idCheck)
         { 
             return null;
         }
    
         return await _pgSqlContext.PizzasEntity
             .Where(p => p.Id == id)
             .FirstOrDefaultAsync(cancellationToken: cancellationToken);
    
    }

    public async Task CreatePizzasAsync(CreatePizzaModel createPizzaModel, string email,
        CancellationToken cancellationToken)
    {
        // var customer = await _customerRepository
        //     .GetCustomerByEmailAsync(email, cancellationToken);
        // var cart = await _cartRepository
        //     .GetCartFromCustomerId(customer.Id, cancellationToken);

        // var customer = await _accountRepository.GetCustomerByEmailAsync(email, cancellationToken);
        var cart = await _accountRepository.GetCartFromCustomerId(email, cancellationToken);
        
        await _pgSqlContext.PizzasEntity.AddAsync(Mappers.CreatePizzaModelToPizzasEntity(createPizzaModel, cart.Id),
            cancellationToken);
        var pizzas = await GetPizzasFromCartAsync(email, cancellationToken);

        await _cartRepository.UpdatePrice(email, pizzas.Sum(s => s.Price), cancellationToken);
        await _cartRepository.UpdateQuantity(email, pizzas.Sum(s => s.Quantity), cancellationToken);
        await _pgSqlContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<List<PizzasEntity>> GetPizzasFromCartAsync(string email, CancellationToken cancellationToken)
    {
        // var customer = await _customerRepository
        //     .GetCustomerByEmailAsync(email, cancellationToken);
        // var cart = await _cartRepository
        //     .GetCartFromCustomerId(customer.Id, cancellationToken);

        var cart = await _accountRepository.GetCartFromCustomerId(email, cancellationToken);

        return await _pgSqlContext.PizzasEntity.Where(c => c.CartId == cart.Id).ToListAsync(cancellationToken);
    }

    public async Task PutPizzasAsync(string id ,PutPizzaModel putPizzaModel, CancellationToken cancellationToken)
    {
        var pizza = await GetPizzasByIdAsync(id, new CancellationToken());
        Mappers.PizzasEntityToPutPizzaModel(pizza, putPizzaModel);
        await _pgSqlContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeletePizzasAsync(PizzasEntity pizza, CancellationToken cancellationToken)
    {
        _pgSqlContext.PizzasEntity.Remove(pizza);

        await _pgSqlContext.SaveChangesAsync(cancellationToken);
    }
}