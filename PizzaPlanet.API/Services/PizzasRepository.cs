using System.Net;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using PizzaPlanet.API.Commons;
using PizzaPlanet.API.Context;
using PizzaPlanet.API.Entities;
using PizzaPlanet.API.Models;
using PizzaPlanet.API.Services.Interfaces;

namespace PizzaPlanet.API.Services;

public class PizzasRepository : IPizzaRepository
{
    private readonly MongoDbContext _mongoDbContext;
    
    private IMongoCollection<PizzasEntity> MongoCollection => _mongoDbContext.GetCollection<PizzasEntity>("pizzas");


    public PizzasRepository(MongoDbContext mongoDbContext)
    {
        _mongoDbContext = mongoDbContext;
    }
    
    public async Task<List<PizzasEntity>> GetAllPizzasAsync(CancellationToken cancellationToken)
    {
        return await MongoCollection.Find(_ => true).ToListAsync(cancellationToken: cancellationToken);
    }

    public async Task<List<PizzasEntity>> GetPizzasByIdAsync(string id)
    {
        var idCheck = await MongoCollection.Find(_ => _.Id == id).AnyAsync();

        if (!idCheck) return null;
        var pizza = MongoCollection.Find(p => p.Id == id);
        return await pizza.ToListAsync();
    }

    public async Task CreatePizzaAsync(CreatePizzaModel createPizzaModel,CancellationToken cancellationToken)
    {
        // var orders = _mongoDbContext.GetCollection<PizzasEntity>("pizzas");
        await MongoCollection.InsertOneAsync(Mappers.CreatePizzaModelToPizzasEntity(createPizzaModel), cancellationToken: cancellationToken);
    }

    public async Task UpdatePizzaAsync(string id ,PutPizzaModel putPizzaModel, CancellationToken cancellationToken)
    {
        // Define the filter
        var filter = Builders<PizzasEntity>.Filter.Eq("_id", ObjectId.Parse(id));
        // Preserve CreatedAt date timestamp to pass through mapper.
        var date = await MongoCollection.Find(filter).FirstOrDefaultAsync(cancellationToken: cancellationToken);
        
        // Update the document
        await MongoCollection.ReplaceOneAsync(filter, Mappers.PutPizzaModelToPizzasEntity(putPizzaModel, date.CreatedAt), cancellationToken: cancellationToken);
    }

    public async Task<object> DeletePizzaAsync(string id, CancellationToken cancellationToken)
    {
        var idCheck = await MongoCollection.Find(_ => _.Id == id).AnyAsync(cancellationToken: cancellationToken);

        if (!idCheck) return null;
        var filteredById = Builders<PizzasEntity>.Filter.Eq("_id", ObjectId.Parse(id));
        var deleted = await MongoCollection.DeleteOneAsync(filteredById, cancellationToken);
        return deleted;
    }
}