using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PizzaPlanet.API.Entities;

public class PizzasEntity
{
    private decimal _price = 12.99m;

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]    
    public string Id { get; init; }
    public string CrustType { get; set; }
    public string Size { get; set; }

    public decimal Price
    {
        // TODO Price not showing as it showed in DB
        get => _price;
        set
        {
            _price = value;
            _price = Size switch
        {
            "S" => 5.99m,
            "M" => 7.99m,
            "L" => 9.99m,
            _ => Price 
        };
        }
    }

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