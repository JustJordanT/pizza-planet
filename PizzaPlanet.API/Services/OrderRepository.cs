using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Extensions;
using PizzaPlanet.API.Commons;
using PizzaPlanet.API.Context;
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

    public async Task UpdateOrderStatus(string email,OrderStatus orderStatus, CancellationToken cancellationToken)
    {
        var order = await _accountRepository.GetOrderFromCartId(email, cancellationToken);
        order.OrderStatus = nameof(orderStatus);
        _pgSqlContext.SaveChangesAsync(cancellationToken);
    }
}