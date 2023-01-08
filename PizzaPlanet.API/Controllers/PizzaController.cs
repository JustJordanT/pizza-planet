using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using PizzaPlanet.API.Commons;
using PizzaPlanet.API.Context;
using PizzaPlanet.API.Entities;
using PizzaPlanet.API.Models;

namespace PizzaPlanet.API.Controllers;

[ApiController]
[Route("api/pizzas")]
public class PizzaController : ControllerBase
{
    private readonly MongoDbContext _mongoDbContext;

    public PizzaController(MongoDbContext mongoDbContext)
    {
        _mongoDbContext = mongoDbContext;
    }

    [HttpGet("{id}")]
    public ActionResult GetPizzaById(string id)
    {
        var orders = _mongoDbContext.GetCollection<PizzasEntity>("pizzas");
        var filteredById = Builders<PizzaModel>.Filter.Eq("_id",ObjectId.Parse(id));
        return Ok(filteredById);
    }

    [HttpGet]
    public ActionResult<GetPizzaModel> GetPizzas([FromQuery] int count = 10)
    {
        var orders = _mongoDbContext.GetCollection<PizzasEntity>("pizzas");
        var pizzas = orders.Find(_ => true);
        
        return Ok(pizzas.ToList().Take(count));
        

    }

    [HttpPost]
    public ActionResult CreatePizza([FromBody] PizzaModel pizza)
    {
        var orders = _mongoDbContext.GetCollection<PizzasEntity>("pizzas");
        orders.InsertOne(Mappers.PizzaModelToPizzasEntity(pizza));
        return Created("", pizza);
    }

    [HttpPut("{id}")]
    public ActionResult UpdatePizza(string id, [FromBody] PutPizzaModel pizza)
    {
        var orders = _mongoDbContext.GetCollection<PizzasEntity>("pizzas");
        // var pizzas = orders.Find(c => c.Id == id);
        // orders.UpdateOne(c =);
        
        // Define the filter
        var filter = Builders<PizzasEntity>.Filter.Eq("_id", ObjectId.Parse(id));
        // Preserve CreatedAt date timestamp to pass through mapper.
        var date = orders.Find(filter).FirstOrDefault();

        // Update the document
        orders.ReplaceOne(filter, Mappers.PutPizzaModelToPizzasEntity(pizza, date.CreatedAt));
        
        return NoContent();
    }

    [HttpDelete]
    public ActionResult DeletePizza([FromQuery] string Id)
    {
        var orders = _mongoDbContext.GetCollection<PizzaModel>("pizzas");
        var filteredById = Builders<PizzaModel>.Filter.Eq("_id", ObjectId.Parse(Id));
        orders.DeleteOne(filteredById);
        return NoContent();
    }
}