using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using PizzaPlanet.API.Commons;
using PizzaPlanet.API.Models;
using PizzaPlanet.API.Services.Interfaces;

namespace PizzaPlanet.API.Controllers
{
    [Route("api/customers")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerController(ICustomerRepository customerRepository, IAuthenticationRepository authenticationRepository)
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
            _customerRepository.CreateCustomerAsync(createCustomer, new CancellationToken());
            return Created("", createCustomer);
        }

        [HttpPost("login")]
        public async Task<ActionResult<bool>> Login(LoginCustomer customer)
        {
            if (!await _customerRepository.GetCustomerByEmail(customer.Email))
            {
                return NotFound("Customer not found by email address, please try again");
            }

            if (!await _customerRepository.VerifyCustomerPassword(customer.Email, customer.Password))
            {
                return BadRequest("Password Incorrect; or invalid");
            }

            var token = _customerRepository.CreateToken(customer);
            
            return Ok(token);
        }
    }
}
