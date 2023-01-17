using System.Drawing;
using Microsoft.AspNetCore.Components.Web;
using PizzaPlanet.API.Entities;
using PizzaPlanet.API.Models;

namespace PizzaPlanet.API.Commons;

public abstract class Mappers
{
   public static PizzasEntity CreatePizzaModelToPizzasEntity(CreatePizzaModel createPizzaModel, string cartId)
   {
      // private readonly Dictionary<string, decimal> prices;
      //
      var prices  = new Dictionary<string, decimal>
      {
         { "S", 6.99m },
         { "M", 7.99m },
         { "L", 9.99m },
      };
      
      var passedModel = new PizzasEntity
      {
         CrustType = createPizzaModel.CrustType,
         Size = createPizzaModel.Size,
         CartId = cartId,
         Price = prices[createPizzaModel.Size] * createPizzaModel.Quantity,
         Toppings = createPizzaModel.Toppings,
         IsGlutenFree = createPizzaModel.IsGlutenFree,
         IsVegan = createPizzaModel.IsVegan,
         IsVegetarian = createPizzaModel.IsVegetarian,
         Quantity = createPizzaModel.Quantity
      };
      
      return passedModel;
   }
   
   public static void PizzasEntityToPutPizzaModel(PizzasEntity pizzasEntity, PutPizzaModel putPizzaModel)
   {
      pizzasEntity.CrustType = putPizzaModel.CrustType;
      pizzasEntity.Size = putPizzaModel.Size;
      pizzasEntity.Toppings = putPizzaModel.Toppings;
      pizzasEntity.IsVegan = putPizzaModel.IsVegan;
      pizzasEntity.IsGlutenFree = putPizzaModel.IsGlutenFree;
      pizzasEntity.IsVegetarian = putPizzaModel.IsVegetarian;
      pizzasEntity.Quantity = putPizzaModel.Quantity;
      pizzasEntity.UpdatedAt = DateTime.UtcNow;
   }  
   
   
   // public static CartEntity PutCartModelToCartEntity(PutCartModel putCartModel, decimal pizzaPriceTotal, int pizzaQuantity, DateTime date)
   // {
   //
   //    var pizzaIds = putCartModel.PizzasEntities.ToList();
   //
   //    var passedCart = new CartEntity
   //    {
   //       Id = putCartModel.Id,
   //       PizzasEntities = pizzaIds,
   //       CustomerId = putCartModel.CustomerId,
   //       Price = pizzaPriceTotal,
   //       Quantity = pizzaQuantity,
   //       CreatedAt = date
   //    };
   //    
   //    return passedCart;
   // }
   
   public static void PutCustomerModelToCustomerEntity(CustomerEntity customerEntity,
      PutCustomerModel putCustomerModel,
      string passwordCrypt,
      string passwordHash,
      string passwordSalt)
   {
         customerEntity.Name = putCustomerModel.Name;
         customerEntity.Email = putCustomerModel.Email;
         customerEntity.Password = passwordCrypt;
         customerEntity.PasswordHash = passwordHash;
         customerEntity.PasswordSalt = passwordSalt;
         customerEntity.UpdatedAt = DateTime.UtcNow;
   }

   public static List<GetPizzaModel> ListOfPizzasEntitiesToListOfPizzasModel(IEnumerable<PizzasEntity> pizzasEntity)
   {
      return pizzasEntity.Select(pizza => new GetPizzaModel
         {
            Id = pizza.Id,
            CartId = pizza.CartId,
            CrustType = pizza.CrustType,
            Size = pizza.Size,
            Price = pizza.Price,
            Toppings = pizza.Toppings,
            IsGlutenFree = pizza.IsGlutenFree,
            IsVegan = pizza.IsVegan,
            IsVegetarian = pizza.IsVegetarian,
            Quantity = pizza.Quantity
         })
         .ToList();
   }

   public static GetPizzaModel PizzaEntityToPizzaModel(PizzasEntity pizzasEntity)
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
         Quantity = pizzasEntity.Quantity
      };
      return passedEntity;
   }

   public static CustomerEntity CreateCustomerToCustomerEntity(CreateCustomer customer, string passwordCrypt ,string hash, string salt)
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