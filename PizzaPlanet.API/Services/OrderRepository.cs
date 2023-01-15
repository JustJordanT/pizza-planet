using Microsoft.EntityFrameworkCore;
using PizzaPlanet.API.Commons;
using PizzaPlanet.API.Context;
using PizzaPlanet.API.Models;
using PizzaPlanet.API.Services.Interfaces;

namespace PizzaPlanet.API.Services;

public class OrderRepository : IOrderRepository
{
    private readonly PgSqlContext _pgSqlContext;

    public OrderRepository(PgSqlContext pgSqlContext)
    {
        _pgSqlContext = pgSqlContext ?? throw new ArgumentNullException(nameof(pgSqlContext));
    }
    
    public async Task InitCustomerOrder(CreateCustomer createCustomer, CancellationToken cancellationToken)
    {
        var customer = await _pgSqlContext.CustomerEntity
            .FirstOrDefaultAsync(c => c.Email == createCustomer.Email, cancellationToken: cancellationToken);
        
        var cart = await _pgSqlContext.CartEntity.FirstOrDefaultAsync(c => c.CustomerId == customer.Id, cancellationToken: cancellationToken); 
        
        // await CreateCartAsync(customer.Id, cancellationToken);
        await _pgSqlContext.OrderEntity.AddAsync(OrderMapper.InitOrder(cart.Id), cancellationToken);
        await _pgSqlContext.SaveChangesAsync(cancellationToken);
    }
}