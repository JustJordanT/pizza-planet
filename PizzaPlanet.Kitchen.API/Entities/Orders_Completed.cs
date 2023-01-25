using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;

namespace PizzaPlanet.Kitchen.API.Entities;

public class Orders_Completed
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string Id { get; init; }
    public string OrderId { get; set; }
    public string CookId { get; set; }
    public string OrderStatus { get; set; }
    public CooksEntity CookEntity { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    
}