using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Extensions;
using PizzaPlanet.API.Commons;
using PizzaPlanet.API.Context;
using PizzaPlanet.API.Entities;
using PizzaPlanet.API.Models;
using PizzaPlanet.API.Services.Interfaces;

namespace PizzaPlanet.API.Services;

public class CartRepository : ICartRepository
{
    private readonly PgSqlContext _pgSqlContext;
    private readonly IAccountRepository _accountRepository;

    private readonly IOrderRepository _orderRepository;
    // private readonly IPizzaRepository _pizzaRepository;

    public CartRepository(
        PgSqlContext pgSqlContext,
        IAccountRepository accountRepository,
        IOrderRepository orderRepository)
    {
        _pgSqlContext = pgSqlContext ?? throw new ArgumentNullException(nameof(pgSqlContext));
        _accountRepository = accountRepository ?? throw new ArgumentNullException(nameof(accountRepository));
        _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
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

    public async Task UpdateQuantity(string email, int quantity, CancellationToken cancellationToken)
    {
        var cart = await _accountRepository.GetCartFromCustomerId(email, cancellationToken);
        cart.Quantity = quantity;
        await  _pgSqlContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdatePrice(string email, decimal price, CancellationToken cancellationToken)
    {
        var cart = await _accountRepository.GetCartFromCustomerId(email, cancellationToken);
        cart.Price = price;
        await  _pgSqlContext.SaveChangesAsync(cancellationToken);
    }

    public async Task CreateCartAsync(string initCart, CancellationToken cancellationToken)
    {
        await _pgSqlContext.CartEntity.AddAsync(CartMapper.InitCartModelToCartEntity(initCart), cancellationToken);
    }

    public async Task SubmitCart(string email, CancellationToken cancellationToken)
    {
        var cart = await _accountRepository.GetCartFromCustomerId(email, cancellationToken);
        await _orderRepository.UpdateOrderStatus(cart , email, cancellationToken);
        cart.IsActive = false;
        await _pgSqlContext.SaveChangesAsync(cancellationToken);
        
        await ResetCartAsync(email, cancellationToken);
        var cartNew = await _accountRepository.GetCartFromCustomerId(email, cancellationToken);
        await _orderRepository.ResetOrderAsync(cartNew, email, cancellationToken);

        await _pgSqlContext.SaveChangesAsync(cancellationToken);
    }

    public async Task ResetCartAsync(string email, CancellationToken cancellationToken)
    {
        var customer = await _accountRepository.GetCustomerByEmailAsync(email, cancellationToken);
        await _pgSqlContext.CartEntity.AddAsync(CartMapper.InitCartModelToCartEntity(customer.Id), cancellationToken);
        await _pgSqlContext.SaveChangesAsync(cancellationToken);
    }
}