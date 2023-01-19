using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Extensions;
using PizzaPlanet.API.Commons;
using PizzaPlanet.API.Context;
using PizzaPlanet.API.Entities;
using PizzaPlanet.API.Models;
using PizzaPlanet.API.Services.Interfaces;

namespace PizzaPlanet.API.Services;

public class OrderRepository : IOrderRepository
{
    private readonly PgSqlContext _pgSqlContext;
    private readonly IAccountRepository _accountRepository;

    public OrderRepository(PgSqlContext pgSqlContext, IAccountRepository accountRepository)
    {
        _pgSqlContext = pgSqlContext ?? throw new ArgumentNullException(nameof(pgSqlContext));
        _accountRepository = accountRepository ?? throw new ArgumentNullException(nameof(accountRepository));
    }
    
    public async Task InitCustomerOrder(CreateCustomer createCustomer, CancellationToken cancellationToken)
    {
        var customer = await _pgSqlContext.CustomerEntity
            .FirstOrDefaultAsync(c => c.Email == createCustomer.Email, cancellationToken: cancellationToken);
        
        var cart = await _pgSqlContext.CartEntity.FirstOrDefaultAsync(c => c.CustomerId == customer.Id, cancellationToken: cancellationToken);

        await _pgSqlContext.OrderEntity.AddAsync(OrderMapper.InitOrder(cart.Id), cancellationToken);
        await _pgSqlContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateOrderStatus(CartEntity cart, string email, CancellationToken cancellationToken)
    {
        var order = await _accountRepository.GetOrderFromCartId(cart ,email, cancellationToken);
        if (order == null) throw new ArgumentNullException(nameof(order));
        order.OrderStatus = nameof(OrderStatus.OrderPending);
        order.UpdatedAt = DateTime.UtcNow;
        await _pgSqlContext.SaveChangesAsync(cancellationToken);
    }
    
    public async Task ResetOrderAsync(CartEntity cart, string email, CancellationToken cancellationToken)
    {
        await _pgSqlContext.OrderEntity.AddAsync(OrderMapper.InitOrder(cart.Id), cancellationToken);
        await _pgSqlContext.SaveChangesAsync(cancellationToken);
    }
}