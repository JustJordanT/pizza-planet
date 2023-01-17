using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
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
    private readonly IPizzaRepository _pizzasRepository;
    private readonly IAuthenticationRepository _authenticationRepository;

    public PizzaController(IPizzaRepository pizzasRepository, IAuthenticationRepository authenticationRepository)
    {
        _pizzasRepository = pizzasRepository ?? throw new ArgumentNullException(nameof(pizzasRepository));
        _authenticationRepository = authenticationRepository ?? throw new ArgumentNullException(nameof(authenticationRepository));
    }

    // [HttpGet("{id}")]
    // public async Task<ActionResult<GetPizzaModel>> GetPizzaById(string id)
    // {
    //     var pizzas = await _pizzasRepository.GetPizzasByIdAsync(id ,new CancellationToken());
    //     if (pizzas == null)
    //     {
    //         return NotFound($"A item with id of: {id} was not found, please try again");
    //     }
    //
    //     return Ok(Mappers.PizzaEntityToPizzaModel(pizzas));
    // }
    //
    // [HttpGet]
    // public async Task<ActionResult<GetPizzaModel>> GetPizzas(CancellationToken cancellationToken)
    // {
    //     var pizzas = await _pizzasRepository.GetAllPizzasAsync(cancellationToken);
    //     return Ok(Mappers.ListOfPizzasEntitiesToListOfPizzasModel(pizzas));
    // }
    //
    [HttpGet]
    public async Task<OkObjectResult> GetPizzasFromCart()
    {
        var currentEmail = _authenticationRepository.GetCurrentEmail(Request.Headers["Authorization"]);
        // return Ok(await _pizzasRepository.GetPizzasFromCartAsync(currentEmail, new CancellationToken()));
        var pizzas = await _pizzasRepository.GetPizzasFromCartAsync(currentEmail, new CancellationToken());
        return Ok(Mappers.ListOfPizzasEntitiesToListOfPizzasModel(pizzas));
    }

    [HttpPost]
    public async Task<ActionResult> CreatePizza([FromBody] CreatePizzaModel createPizza)
    {
        var currentEmail = _authenticationRepository.GetCurrentEmail(Request.Headers["Authorization"]);
        

        if (createPizza == null)
        {
            return BadRequest();
        }
    
        await _pizzasRepository.CreatePizzasAsync(createPizza, currentEmail,new CancellationToken());

        return Created("", createPizza);

    }
    //
    // [HttpPut("{id}")]
    // public async Task<ActionResult> UpdatePizza(string id, [FromBody] PutPizzaModel pizza)
    // {
    //     await _pizzasRepository.PutPizzasAsync(id, pizza, new CancellationToken());
    //     return NoContent();
    // }
    //
    // [HttpDelete("{id}")]
    // public async Task<ActionResult> DeletePizza(string id)
    // {
    //     var deleted = _pizzasRepository.DeletePizzasAsync(id, new CancellationToken());
    //     if (deleted == null)
    //     {
    //         return NotFound($"A item with id of: {id}; was not found, please try again");
    //     }
    //     return NoContent();
    // }
}