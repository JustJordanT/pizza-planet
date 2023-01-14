using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using MongoDB.Driver;
using PizzaPlanet.API.Commons;
using PizzaPlanet.API.Context;
using PizzaPlanet.API.Entities;
using PizzaPlanet.API.Models;
using PizzaPlanet.API.Services.Interfaces;

namespace PizzaPlanet.API.Services;

public class CustomerRepository : ICustomerRepository
{
    private readonly IAuthenticationRepository _authenticationRepository;
    private readonly PgSqlContext _pgSqlContext;
    private readonly ICartRepository _cartRepository;

    public CustomerRepository(
        IAuthenticationRepository authenticationRepository,
        PgSqlContext pgSqlContext,
        ICartRepository cartRepository)
    {
        _authenticationRepository = authenticationRepository ?? throw new ArgumentNullException(nameof(authenticationRepository));
        _pgSqlContext = pgSqlContext ?? throw new ArgumentNullException(nameof(pgSqlContext));
        _cartRepository = cartRepository ?? throw new ArgumentNullException(nameof(cartRepository));
    }

    public async Task CreateCustomerAsync(CreateCustomer customer, CancellationToken cancellationToken)
    {
        _authenticationRepository.CreatePasswordCrypt(
            customer.Password,
            out var passwordCrypt,
            out var passwordHash,
            out var passwordSalt);
         await _pgSqlContext.CustomerEntity.AddAsync(
            Mappers.CreateCustomerToCustomerEntity(
                customer,
                passwordCrypt,
                passwordHash,
                passwordSalt)
            , cancellationToken: cancellationToken);
        await _pgSqlContext.SaveChangesAsync(cancellationToken);
        await _cartRepository.InitCustomerCart(customer, customer.Email, cancellationToken);

    }
    //
    // public async Task<bool> GetCustomerByEmail(string email)
    // {
    //     // var emailCheck = await MongoCollection.Find(_ => _.Email == email).AnyAsync();
    //     return await _pgSqlContext.CustomerEntity.AnyAsync(c => c.Email == email);
    // }
    //
    // public async Task<bool> VerifyCustomerPassword(string email, string password)
    // {
    //     var user = await _pgSqlContext.CustomerEntity.FirstOrDefaultAsync(c => c.Email == email); 
    //     
    //     return await _authenticationRepository.VerifyPassword(password, user.Password);
    // }
    //
    // public string CreateToken(LoginCustomer customer)
    // {
    //     return _authenticationRepository.GenerateJwtToken(customer);
    // }
    //
    // public async Task<List<CustomerEntity>> GetCustomersAsync(CancellationToken cancellationToken)
    // {
    //     return await _pgSqlContext.CustomerEntity.ToListAsync(cancellationToken);
    // }
    //
    // public async Task<CustomerEntity> GetCustomersByIdAsync(string id, CancellationToken cancellationToken)
    // {
    //     var customerCheck = await _pgSqlContext.CustomerEntity.AnyAsync(c => c.Id == id, cancellationToken: cancellationToken);
    //
    //     if (!customerCheck) return null; 
    //     return await _pgSqlContext.CustomerEntity.FirstOrDefaultAsync(c => c.Id == id, cancellationToken);       
    // }
    //
    // // TODO put not working... idcheck is always returning false
    // public async Task<CustomerEntity> PutCustomersAsync(string id, PutCustomerModel putCustomerModel,
    //     CancellationToken cancellationToken)
    // {
    //     _authenticationRepository.CreatePasswordCrypt(
    //         putCustomerModel.Password,
    //         out var passwordCrypt,
    //         out var passwordHash,
    //         out var passwordSalt);
    //     // Preserve CreatedAt date timestamp to pass through mapper.
    //
    //     var idCheck = await _pgSqlContext.CustomerEntity.AnyAsync(c => c.Id == id, cancellationToken);
    //     if (idCheck)
    //     {
    //         var customer = await _pgSqlContext.CustomerEntity
    //             .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
    //         Mappers.PutCustomerModelToCustomerEntity(customer,
    //             putCustomerModel,
    //             passwordCrypt,
    //             passwordHash,
    //             passwordSalt);
    //         await _pgSqlContext.SaveChangesAsync(cancellationToken); 
    //     }
    //
    //     return null;
    // }

    // public async Task<object> DeleteCustomersAsync(string id, CancellationToken cancellationToken)
    // {
    //     var idCheck = await _pgSqlContext.CustomerEntity.AnyAsync(c => c.Id == id, cancellationToken: cancellationToken); 
    //
    //     if (!idCheck) return null;
    //     var filteredById = await _pgSqlContext.CustomerEntity.FirstOrDefaultAsync(c => c.Id == id, cancellationToken: cancellationToken);
    //     var deleted = _pgSqlContext.CustomerEntity.Remove(filteredById);
    //     await _pgSqlContext.SaveChangesAsync(cancellationToken);
    //     return deleted;
    // }
}