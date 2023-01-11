using System.Security.Claims;
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
    private readonly MongoDbContext _mongoDbContext;
    private readonly IAuthenticationRepository _authenticationRepository;

    private IMongoCollection<CustomerEntity> MongoCollection => _mongoDbContext.GetCollection<CustomerEntity>("customers");


    public CustomerRepository(MongoDbContext mongoDbContext, IAuthenticationRepository authenticationRepository)
    {
        _mongoDbContext = mongoDbContext ?? throw new ArgumentNullException(nameof(mongoDbContext));
        _authenticationRepository = authenticationRepository ?? throw new ArgumentNullException(nameof(authenticationRepository));
    }

    public async Task CreateCustomerAsync(CreateCustomer customer, CancellationToken cancellationToken)
    {
        // _authenticationRepository.CreatePasswordHash(customer.Password, out var passwordHash, out var passwordSalt);
        _authenticationRepository.CreatePasswordCrypt(customer.Password,out var passwordCrypt, out var passwordHash, out var passwordSalt);
        // var user = Mappers.CreateCustomerToCustomerEntity(customer, passwordHash, passwordSalt);
        // await MongoCollection.InsertOneAsync(Mappers.CreateCustomerToCustomerEntity(customer, passwordHash, passwordSalt), cancellationToken: cancellationToken);
        await MongoCollection.InsertOneAsync(Mappers.CreateCustomerToCustomerEntity(customer, passwordCrypt,passwordHash, passwordSalt), cancellationToken: cancellationToken);
    }

    public async Task<bool> GetCustomerByEmail(string email)
    {
        // var emailCheck = await MongoCollection.Find(_ => _.Email == email).AnyAsync();
        return await MongoCollection.Find(_ => _.Email == email).AnyAsync();
    }

    public async Task<bool> VerifyCustomerPassword(string email, string password)
    {
        var filter = Builders<CustomerEntity>.Filter.Eq("email", email);
        var user = await MongoCollection.Find(filter).FirstOrDefaultAsync();

        return await _authenticationRepository.VerifyPassword(password, user.Password);
    }

    public string CreateToken(LoginCustomer customer)
    {
        return _authenticationRepository.GenerateJwtToken(customer);
    }

}