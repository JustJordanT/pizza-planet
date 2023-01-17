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
    {
        _pgSqlContext = pgSqlContext ?? throw new ArgumentNullException(nameof(pgSqlContext));
    }

    public async Task InitCustomerCart(CreateCustomer createCustomer, CancellationToken cancellationToken)
    {
        var customer = await _pgSqlContext.CustomerEntity
            .FirstOrDefaultAsync(c => c.Email == createCustomer.Email, cancellationToken: cancellationToken);
        
        // await CreateCartAsync(customer.Id, cancellationToken);
        await _pgSqlContext.CartEntity.AddAsync(CartMapper.InitCartModelToCartEntity(customer.Id), cancellationToken);

        
        await _pgSqlContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<CartEntity> GetCartFromCustomerId(string customerId, CancellationToken cancellationToken)
    {
       return await _pgSqlContext.CartEntity.FirstOrDefaultAsync(cart => cart.CustomerId == customerId, cancellationToken: cancellationToken);
    }

    public async Task UpdateQuantity(string customerId, int quantity, CancellationToken cancellationToken)
    {
        var cart = await GetCartFromCustomerId(customerId, cancellationToken);
        cart.Quantity = quantity;
        // await _pgSqlContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdatePrice(string customerId, decimal price, CancellationToken cancellationToken)
    {
        var cart = await GetCartFromCustomerId(customerId, cancellationToken);
        cart.Price = price;
        // await _pgSqlContext.SaveChangesAsync(cancellationToken);
    }

    // public async Task AddPizzaToCart(string pizzaId, CancellationToken cancellationToken)
    // {
    //     await _pgSqlContext.CartEntity
    // }

    public async Task CreateCartAsync(string initCart, CancellationToken cancellationToken)
    {
        await _pgSqlContext.CartEntity.AddAsync(CartMapper.InitCartModelToCartEntity(initCart), cancellationToken);
    }
    
    
}