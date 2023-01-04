using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using PizzaPlanet.API.Context;
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
        var orders = _mongoDbContext.GetCollection<PizzaModel>("pizzas");
        var filteredById = Builders<PizzaModel>.Filter.Eq("_id",ObjectId.Parse(id));
        return Ok(filteredById);
    }

    [HttpGet]
    public ActionResult GetPizzas([FromQuery] int count = 10)
    {
        var orders = _mongoDbContext.GetCollection<PizzaModel>("pizzas");
        var pizzas = orders.Find(_ => true);
        return Ok(pizzas.ToList().Take(count));
    }

    [HttpPost]
    public ActionResult CreatePizza([FromBody] PizzaModel pizza)
    {
        var orders = _mongoDbContext.GetCollection<PizzaModel>("pizzas");
        orders.InsertOne(pizza);
        return Created("", pizza);
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