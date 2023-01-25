using PizzaPlanet.Kitchen.API.Entities;
using PizzaPlanet.Kitchen.API.Models;

namespace PizzaPlanet.Kitchen.API.Services.Interfaces;

public interface ICooksRepository
{
   Task<CooksEntity> CreateCookAsync(CreateCookModel createCook, CancellationToken cancellationToken);
   Task<bool> CooksExistAsync();
   
   // Gets a cook to prepare pizza
   Task<string> GetRandomCookId();
}