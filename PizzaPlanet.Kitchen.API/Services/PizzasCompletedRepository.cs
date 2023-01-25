using PizzaPlanet.Kitchen.API.Commons.Mapper;
using PizzaPlanet.Kitchen.API.Context;
using PizzaPlanet.Kitchen.API.Models;

namespace PizzaPlanet.Kitchen.API.Services.Interfaces;

public class PizzasCompletedRepository : IPizzasCompletedRepository
{
   private readonly KitchenDbContext _kitchenDbContext;

   public PizzasCompletedRepository(KitchenDbContext kitchenDbContext)
   {
      _kitchenDbContext = kitchenDbContext;
   }
   
   public async Task PizzasCompleted(string pizzaId, string cookId)
   {
      var pizza = new CreatePizzasCompleted
      {
         PizzaId = pizzaId,
         CookId = cookId
      };
      
      await _kitchenDbContext.PizzasCompleted.AddAsync(PizzaMapper.CreatePizzasCompleted(pizza));
      
   }
}