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
    private readonly MongoDbContext _mongoDbContext;
    private readonly PgSqlContext _pgSqlContext;
    private readonly IPizzaRepository _pizzaRepository;

    private IMongoCollection<CartEntity> MongoCollection => _mongoDbContext.GetCollection<CartEntity>("carts");


    public CartRepository(
        MongoDbContext mongoDbContext,
        PgSqlContext pgSqlContext,
        IPizzaRepository pizzaRepository)
    {
        _mongoDbContext = mongoDbContext;
        _pgSqlContext = pgSqlContext ?? throw new ArgumentNullException(nameof(pgSqlContext));
        _pizzaRepository = pizzaRepository;
    }
    
}