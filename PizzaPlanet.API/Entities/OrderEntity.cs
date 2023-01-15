using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;
using PizzaPlanet.API.Entities;

namespace PizzaPlanet.API.Models;

public class OrderEntity
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string Id { get; init; }
    public string CartId { get; set; }
    public CartEntity Cart { get; set; }
    public string OrderStatus { get; set; } = "Ordering";
    public DateTime CreatedAt { get; init; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}