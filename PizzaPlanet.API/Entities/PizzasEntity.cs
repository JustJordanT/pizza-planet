using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace PizzaPlanet.API.Entities;

public class PizzasEntity
{
    // private readonly Dictionary<string, decimal> prices;
    // private decimal _price;

    // public PizzasEntity()
    // {
    //     prices = new Dictionary<string, decimal>
    //     {
    //         { "S", 6.99m },
    //         { "M", 7.99m },
    //         { "L", 9.99m },
    //         { null, 12.99m }
    //     };
    // }

    // public PizzasEntity()
    // {
    //     Price = Size.ToUpper() switch
    //     {
    //         "S" => 10,
    //         "M" => 15,
    //         "L" => 20,
    //         _ => throw new ArgumentException("Invalid size. Size should be S, M, L")
    //     };
    // }

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]    
    public string Id { get; init; }
    public string CrustType { get; set; }
    public string Size { get; set; }

    // public decimal Price
    // {
    //     get => prices[Size] * Quantity;
    //     set => prices[Size] = value;
    // }
    public decimal Price { get; set; }
    // public decimal Price
    // {
    //     get => _price;
    //     set
    //     {
    //         _price = Size switch
    //         {
    //             "S" => 10,
    //             "M" => 15,
    //             "L" => 20,
    //             _ => throw new ArgumentException("Invalid size. Size should be S, M, L")
    //         };
    //     }
    // }

    public List<string> Toppings { get; set; }
    public bool IsGlutenFree { get; set; }
    public bool IsVegan { get; set; }
    public bool IsVegetarian { get; set; }
    public int Quantity { get; set; }
    public string CartId { get; set; }
    public CartEntity Cart { get; set; }
    public DateTime CreatedAt { get; init; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}