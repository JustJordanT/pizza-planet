using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using MongoDB.Driver;
using PizzaPlanet.API.Commons;
using PizzaPlanet.API.Context;
using PizzaPlanet.API.Entities;
using PizzaPlanet.API.Models;
using PizzaPlanet.API.Services.Interfaces;

namespace PizzaPlanet.API.Services;

public class CartRepository : ICartRepository
{
    private readonly PgSqlContext _pgSqlContext;
    private readonly IPizzaRepository _pizzaRepository;

    public CartRepository(
        PgSqlContext pgSqlContext)
    // IPizzaRepository pizzaRepository)
    {
        _pgSqlContext = pgSqlContext ?? throw new ArgumentNullException(nameof(pgSqlContext));
        // _pizzaRepository = pizzaRepository ?? throw new ArgumentNullException(nameof(pizzaRepository));
    }

    public async Task InitCustomerCart(CreateCustomer customer, string initCart, CancellationToken cancellationToken)
    {
        var tmp = await _pgSqlContext.CustomerEntity
            .FirstOrDefaultAsync(c => c.Email == customer.Email, cancellationToken: cancellationToken);
        Console.WriteLine(tmp);
        await CreateCartAsync(tmp.Id, cancellationToken);
        await _pgSqlContext.SaveChangesAsync(cancellationToken);
    }

    public async Task CreateCartAsync(string initCart, CancellationToken cancellationToken)
    {
        await _pgSqlContext.CartEntity.AddAsync(CartMapper.InitCartModelToCartEntity(initCart), cancellationToken);
    }
}