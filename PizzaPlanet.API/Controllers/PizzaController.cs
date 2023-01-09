using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using PizzaPlanet.API.Commons;
using PizzaPlanet.API.Context;
using PizzaPlanet.API.Entities;
using PizzaPlanet.API.Models;
using PizzaPlanet.API.Services;
using PizzaPlanet.API.Services.Interfaces;

namespace PizzaPlanet.API.Controllers;

[ApiController]
[Route("api/pizzas")]
public class PizzaController : ControllerBase
{
    private readonly MongoDbContext _mongoDbContext;
    private readonly IPizzaRepository _pizzasRepository;

    public PizzaController(MongoDbContext mongoDbContext, IPizzaRepository pizzasRepository)
    {
        _mongoDbContext = mongoDbContext ?? throw new ArgumentNullException(nameof(mongoDbContext));
        _pizzasRepository = pizzasRepository ?? throw new ArgumentNullException(nameof(pizzasRepository));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetPizzaById(string id)
    {
        var filtered = await _pizzasRepository.GetPizzasByIdAsync(id);
        if (filtered == null)
        {
            return NotFound($"A item with id of: {id}; was not found, please try again");
        }
        return Ok(filtered);
    }

    [HttpGet]
    public async Task<ActionResult<GetPizzaModel>> GetPizzas(CancellationToken cancellationToken)
    {
        // var orders = _mongoDbContext.GetCollection<PizzasEntity>("pizzas");
        // var pizzas = orders.Find(_ => true).ToList();
        
        var pizzas = await _pizzasRepository.GetAllPizzasAsync(cancellationToken);
        
        return Ok(pizzas);
    }

    [HttpPost]
    public ActionResult CreatePizza([FromBody] PizzaModel pizza)
    {
        if (pizza == null)
        {
            return BadRequest();
        }

        _pizzasRepository.CreatePizzaAsync(pizza, new CancellationToken());
        return Created("", pizza);
    }
    
    [HttpPut("{id}")]
    public ActionResult UpdatePizza(string id, [FromBody] PutPizzaModel pizza)
    {
        // if (pizza.Id == null)
        // {
        //     return BadRequest();
        // }
        
        _pizzasRepository.UpdatePizzaAsync(id, pizza, new CancellationToken());
        
        return NoContent();
    }

    [HttpDelete("{id}")]
    public ActionResult DeletePizza(string id)
    {
        var deleted = _pizzasRepository.DeletePizzaAsync(id, new CancellationToken());
        if (deleted == null)
        {
            return NotFound($"A item with id of: {id}; was not found, please try again");
        }
        return NoContent();
    }
}