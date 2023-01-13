using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace PizzaPlanet.API.Models;

public class GetCartModel
{

    public List<ObjectId> Pizzas { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public string CustomerId { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; init; } = DateTime.UtcNow;
    [BsonElement("updatedAt")] 
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}