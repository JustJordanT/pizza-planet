using Microsoft.OpenApi.Extensions;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Driver;
using PizzaPlanet.API.Entities;
using PizzaPlanet.API.Models;

namespace PizzaPlanet.API.Commons;

public class Mappers
{
   public static PizzasEntity PizzaModelToPizzasEntity(PizzaModel pizzaModel)
   {
      var passedModel = new PizzasEntity
      {
         CrustType = pizzaModel.CrustType,
         Size = pizzaModel.Size,
         Toppings = pizzaModel.Toppings,
         IsGlutenFree = pizzaModel.IsGlutenFree,
         IsVegan = pizzaModel.IsVegan,
         IsVegetarian = pizzaModel.IsVegetarian,
         Quantity = pizzaModel.Quantity,
      };
      
      return passedModel;
   }
   
   public static PizzasEntity PutPizzaModelToPizzasEntity(PutPizzaModel putPizzaModel, DateTime date)
   {
      var passedModel = new PizzasEntity
      {
         Id = putPizzaModel.Id,
         CrustType = putPizzaModel.CrustType,
         Size = putPizzaModel.Size,
         Toppings = putPizzaModel.Toppings,
         IsGlutenFree = putPizzaModel.IsGlutenFree,
         IsVegan = putPizzaModel.IsVegan,
         IsVegetarian = putPizzaModel.IsVegetarian,
         Quantity = putPizzaModel.Quantity,
         CreatedAt = date
      };
      
      return passedModel;
   }

   public static GetPizzaModel PizzasEntityToPizzasModel(PizzasEntity pizzasEntity)
   {
      var passedEntity = new GetPizzaModel
      {
         Id = pizzasEntity.Id,
         CrustType = pizzasEntity.CrustType,
         Size = pizzasEntity.Size,
         Price = pizzasEntity.Price,
         Toppings = pizzasEntity.Toppings,
         IsGlutenFree = pizzasEntity.IsGlutenFree,
         IsVegan = pizzasEntity.IsVegan,
         IsVegetarian = pizzasEntity.IsVegetarian,
         Quantity = pizzasEntity.Quantity,
      };
      
      return passedEntity;
   }
   
   
}