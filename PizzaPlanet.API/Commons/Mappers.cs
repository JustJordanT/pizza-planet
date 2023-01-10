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
         Price = pizzaModel.Price,
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
         Price = putPizzaModel.Price,
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

   public static object PizzasEntityToPizzasModelMongo(IFindFluent<PizzasEntity, PizzasEntity> pizzasEntity)
   {
      var tmp = BsonClassMap.RegisterClassMap<PizzasEntity>(p =>
      {
         // p.AutoMap();
         p.MapIdProperty(c => c.Id).SetElementName("_id");
         p.MapProperty(c => c.CrustType).SetElementName("crustType");
         p.MapProperty(c => c.Size).SetElementName("size");
         p.MapProperty(c => c.Price).SetElementName("price");
         p.MapProperty(c => c.Toppings).SetElementName("toppings");
         p.MapProperty(c => c.IsGlutenFree).SetElementName("isGlutenFree");
         p.MapProperty(c => c.IsVegan).SetElementName("isVegan");
         p.MapProperty(c => c.IsVegetarian).SetElementName("isVegetarian");
         p.MapProperty(c => c.Quantity).SetElementName("quantity");
         // var bson = model.ToBson();
         // var entity = BsonSerializer.Deserialize<Entity>(bson);
      });
      return tmp;
   }
}