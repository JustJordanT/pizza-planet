using Microsoft.OpenApi.Extensions;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Driver;
using PizzaPlanet.API.Entities;
using PizzaPlanet.API.Models;

namespace PizzaPlanet.API.Commons;

public abstract class Mappers
{
   public static PizzasEntity CreatePizzaModelToPizzasEntity(CreatePizzaModel createPizzaModel)
   {
      var passedModel = new PizzasEntity
      {
         CrustType = createPizzaModel.CrustType,
         Size = createPizzaModel.Size,
         Toppings = createPizzaModel.Toppings,
         IsGlutenFree = createPizzaModel.IsGlutenFree,
         IsVegan = createPizzaModel.IsVegan,
         IsVegetarian = createPizzaModel.IsVegetarian,
         Quantity = createPizzaModel.Quantity,
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

   // TODO Was having issues mapping this one with mongoDB, I was trying to remove the timestamp from the model.
   // public static GetPizzaModel PizzasEntityToPizzasModel(PizzasEntity pizzasEntity)
   // {
   //    var passedEntity = new GetPizzaModel
   //    {
   //       Id = pizzasEntity.Id,
   //       CrustType = pizzasEntity.CrustType,
   //       Size = pizzasEntity.Size,
   //       Price = pizzasEntity.Price,
   //       Toppings = pizzasEntity.Toppings,
   //       IsGlutenFree = pizzasEntity.IsGlutenFree,
   //       IsVegan = pizzasEntity.IsVegan,
   //       IsVegetarian = pizzasEntity.IsVegetarian,
   //       Quantity = pizzasEntity.Quantity,
   //    };
   //    
   //    return passedEntity;
   // }

   public static CustomerEntity CreateCustomerToCustomerEntity(CreateCustomer customer,string passwordCrypt ,string hash, string salt)
   {
      var passedCustomer = new CustomerEntity
      {
         Name = customer.Name,
         Email = customer.Email,
         Password = passwordCrypt, 
         PasswordHash = hash,
         PasswordSalt = salt
      };
      return passedCustomer;
   }
}