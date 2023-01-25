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
      if (customer == null) throw new ArgumentNullException(nameof(customer));
      return await _pgSqlContext.CartEntity.Where(cart => cart.CustomerId == customer.Id && cart.IsActive).FirstOrDefaultAsync(cancellationToken);
   }

   public async Task<IEnumerable<string>> GetAllCartsFromCustomerId(string email, CancellationToken cancellationToken)
   {
      var customer = await GetCustomerByEmailAsync(email, cancellationToken);
      if (customer == null) throw new ArgumentNullException(nameof(customer));
      var carts = await _pgSqlContext.CartEntity.Where(cart => cart.CustomerId == customer.Id)
         .ToListAsync(cancellationToken);
      return carts.Select(z => z.Id).ToList();
   }

   public async Task<List<string>> GetPizzasFromCartAsync(string email, CancellationToken cancellationToken)
   {
      var cart = await GetCartFromCustomerId(email, cancellationToken);
      var pizza = await _pgSqlContext.PizzasEntity.Where(c => c.CartId == cart.Id).ToListAsync(cancellationToken);
      return pizza.Select(p => p.Id).ToList();
   } 
   
   public async Task<CustomerEntity> GetCustomerByEmailAsync(string email, CancellationToken cancellationToken)
   {
      if (email == null) throw new ArgumentNullException(nameof(email));
      return await _pgSqlContext.CustomerEntity
         .FirstOrDefaultAsync(customer => customer.Email == email, cancellationToken: cancellationToken);
   }
  
   public async Task<OrderEntity?> GetOrderFromCartId(CartEntity cart, string email, CancellationToken cancellationToken)
   {
      // var cart = await GetCartFromCustomerId(email, cancellationToken);
      // if (cart == null) throw new ArgumentNullException(nameof(cart));
      return await _pgSqlContext.OrderEntity.Where(order => order.CartId == cart.Id).FirstOrDefaultAsync(cancellationToken);
   }

   public async Task<List<OrderEntity?>> GetAllOrdersFromCartId(IEnumerable<string> cartIdList, string email,
      CancellationToken cancellationToken)
   {
      return await _pgSqlContext.OrderEntity.Where(order => cartIdList.Contains(order.CartId)).ToListAsync(cancellationToken);
   }
}