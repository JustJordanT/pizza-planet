using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PizzaPlanet.API.Commons;
using PizzaPlanet.API.Models;
using PizzaPlanet.API.Services.Interfaces;

namespace PizzaPlanet.API.Controllers
{
    [Route("api/orders")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IAuthenticationRepository _authenticationRepository;

        public OrdersController(IOrderRepository orderRepository, IAuthenticationRepository authenticationRepository)
        {
            _orderRepository = orderRepository;
            _authenticationRepository = authenticationRepository;
        }

        [HttpGet]
        public async Task<ActionResult<GetOrders>> GetOrders()
        {
            var currentEmail = _authenticationRepository.GetCurrentEmail(Request.Headers["Authorization"]);
            if (currentEmail == null)
            {
                return BadRequest("Missing authorization header, please add and try again");
            }
            var orders = _orderRepository.GetOrdersStatusAsync(currentEmail, new CancellationToken());
            return Ok(OrderMapper.ListOfOrders(await orders));
        } 
    }
}
