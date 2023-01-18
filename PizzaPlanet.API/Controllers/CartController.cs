using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PizzaPlanet.API.Entities;
using PizzaPlanet.API.Models;
using PizzaPlanet.API.Services.Interfaces;

namespace PizzaPlanet.API.Controllers
{
    [Route("api/carts")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartRepository _cartRepository;
        private readonly IAuthenticationRepository _authenticationRepository;

        public CartController(ICartRepository cartRepository, IAuthenticationRepository authenticationRepository)
        {
            _cartRepository = cartRepository ?? throw new ArgumentNullException(nameof(cartRepository));
            _authenticationRepository = authenticationRepository ?? throw new ArgumentNullException(nameof(authenticationRepository));
        }

        [HttpPost]
        public async Task<ActionResult> SubmitCart()
        {
            var currentEmail = _authenticationRepository.GetCurrentEmail(Request.Headers["Authorization"]);
            await _cartRepository.SubmitCart(currentEmail, new CancellationToken());
            return NoContent();

        }
        // [HttpPost]
        // public async Task<OkObjectResult> CreateCart(CreateCartModel createCart)
        // {
        //     await _cartRepository.CreateCartAsync(createCart, createCart.PizzasEntities, new CancellationToken());
        //     return Ok(createCart);
        // }
        //
        // [HttpGet]
        // public async Task<ActionResult<CartEntity>> GetCarts() => 
        //     Ok(await _cartRepository.GetCartsAsync(new CancellationToken()));
        //
        // [HttpGet("{id}")]
        // public async Task<ActionResult> GetPizzaById(string id)
        // {
        //     var filtered = await _cartRepository.GetCartsByIdAsync(id, new CancellationToken());
        //     if (filtered == null)
        //     {
        //         return NotFound($"A item with id of: {id}; was not found, please try again");
        //     }
        //     return Ok(filtered);
        // }
        //
        // [HttpPut("{id}")]
        // public async Task<ActionResult> PutCart(string id, [FromBody] PutCartModel customer)
        // {
        //     await _cartRepository.PutCartsAsync(customer,id, customer.PizzasEntities,  new CancellationToken());
        //     return NoContent();
        // }

    }
}
