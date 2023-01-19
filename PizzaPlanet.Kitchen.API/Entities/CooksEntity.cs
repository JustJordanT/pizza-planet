using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PizzaPlanet.Kitchen.API.Entities;

public class CooksEntity
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string Id { get; set; }
    public string Name { get; set; }
    public bool IsAvailable { get; set; }
    public List<Orders_Completed> OrdersCompleted { get; set; }
    public List<Pizzas_Completed> PizzasCompleted { get; set; }
    public DateTime CreatedAt { get; init; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}