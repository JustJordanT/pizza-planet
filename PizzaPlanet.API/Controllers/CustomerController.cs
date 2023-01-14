using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using PizzaPlanet.API.Commons;
using PizzaPlanet.API.Entities;
using PizzaPlanet.API.Models;
using PizzaPlanet.API.Services.Interfaces;

namespace PizzaPlanet.API.Controllers
{
    [Route("api/customers")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerController(
            ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
        }

        [HttpPost("register")]
        public async Task<ActionResult<bool>> Register(CreateCustomer createCustomer)
        {
            if (createCustomer == null)
            {
                return BadRequest();
            }
            await _customerRepository.CreateCustomerAsync(createCustomer, new CancellationToken());
            return Created("", createCustomer);
        }
        //
        // [HttpPost("login")]
        // public async Task<ActionResult<string>> Login(LoginCustomer customer)
        // {
        //     if (!await _customerRepository.GetCustomerByEmail(customer.Email))
        //     {
        //         return NotFound("Customer not found by email address, please try again");
        //     }
        //
        //     if (!await _customerRepository.VerifyCustomerPassword(customer.Email, customer.Password))
        //     {
        //         return BadRequest("Password Incorrect; or invalid");
        //     }
        //
        //     var token = _customerRepository.CreateToken(customer);
        //     
        //     return Ok(token);
        // }
        //
        // [HttpGet]
        // public async Task<ActionResult<CustomerEntity>> GetCustomers() => 
        //     Ok(await _customerRepository.GetCustomersAsync(new CancellationToken()));
        //
        // [HttpGet("{id}")]
        // public async Task<ActionResult> GetCustomerById(string id)
        // {
        //     var filtered = await _customerRepository.GetCustomersByIdAsync(id, new CancellationToken());
        //     if (filtered == null)
        //     {
        //         return NotFound($"A item with id of: {id}; was not found, please try again");
        //     }
        //     return Ok(filtered);
        // }
        //
        // [HttpPut("{id}")]
        // public async Task<ActionResult<PutCustomerModel>> UpdateCustomer(string id, [FromBody] PutCustomerModel customer)
        // {
        //     var item = await _customerRepository.PutCustomersAsync(id, customer, new CancellationToken());
        //     if (item == null)
        //     {
        //         return NotFound($"A item with id of: {id}, was not found, please try again");
        //     }
        //     return NoContent();
        // }
        //
        // [HttpDelete("{id}")]
        // public async Task<ActionResult> DeleteCustomer(string id)
        // {
        //     var deleted = await _customerRepository.DeleteCustomersAsync(id, new CancellationToken());
        //     if (deleted == null)
        //     {
        //         return NotFound($"A item with id of: {id}; was not found, please try again");
        //     }
        //
        //     return NoContent();
        // }
    }
}
