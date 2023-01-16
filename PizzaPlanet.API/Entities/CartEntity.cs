using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using PizzaPlanet.API.Models;

namespace PizzaPlanet.API.Entities;

public class CartEntity
{
   [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
   public string Id { get; init; }

   // public string PizzaId { get; set; }
   public List<PizzasEntity> Pizzas { get; set; }
   public decimal Price { get; set; }
   public int Quantity { get; set; }
   public string CustomerId { get; set; }
   public CustomerEntity Customer { get; set; }
   public bool IsActive { get; set; } = true;
   public OrderEntity Order { get; set; }
   
   public DateTime CreatedAt { get; init; } = DateTime.UtcNow;
   public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}