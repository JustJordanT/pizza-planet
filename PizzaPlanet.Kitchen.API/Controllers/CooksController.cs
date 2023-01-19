using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PizzaPlanet.Kitchen.API.Entities;
using PizzaPlanet.Kitchen.API.Models;
using PizzaPlanet.Kitchen.API.Services.Interfaces;

namespace PizzaPlanet.Kitchen.API.Controllers
{
    [Route("api/cooks")]
    [ApiController]
    public class CooksController : ControllerBase
    {
        private readonly ICooksRepository _cooksRepository;

        public CooksController(ICooksRepository cooksRepository)
        {
            _cooksRepository = cooksRepository;
        }
        
        [HttpPost]
        public async Task<ActionResult<CooksEntity>> CreateCooks(CreateCookModel createCook)
        {
           var cook = await _cooksRepository.CreateCookAsync(createCook, new CancellationToken());
            return Created("Get",new {id = cook.Id});
        }

    }
}
