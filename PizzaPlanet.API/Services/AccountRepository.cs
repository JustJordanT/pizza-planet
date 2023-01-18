using Microsoft.EntityFrameworkCore;
using PizzaPlanet.API.Context;
using PizzaPlanet.API.Entities;
using PizzaPlanet.API.Models;
using PizzaPlanet.API.Services.Interfaces;

namespace PizzaPlanet.API.Services;

public class AccountRepository : IAccountRepository
{
   private readonly PgSqlContext _pgSqlContext;
   // private readonly ICustomerRepository _customerRepository;

   public AccountRepository(PgSqlContext pgSqlContext)
   {
      _pgSqlContext = pgSqlContext ?? throw new ArgumentNullException(nameof(pgSqlContext));
   }
   
   public async Task<CartEntity> GetCartFromCustomerId(string email, CancellationToken cancellationToken)
   {
      var customer = await GetCustomerByEmailAsync(email, cancellationToken);
      return await _pgSqlContext.CartEntity.FirstOrDefaultAsync(cart => cart.CustomerId == customer.Id, cancellationToken: cancellationToken);
   }
   
   public async Task<CustomerEntity> GetCustomerByEmailAsync(string email, CancellationToken cancellationToken)
   {
      return await _pgSqlContext.CustomerEntity
         .FirstOrDefaultAsync(customer => customer.Email == email, cancellationToken: cancellationToken);
   }
}