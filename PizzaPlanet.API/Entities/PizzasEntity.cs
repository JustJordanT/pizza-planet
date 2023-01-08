using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver.Linq;

namespace PizzaPlanet.API.Entities;

public class PizzasEntity
{
    [BsonId, BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    [BsonElement("crustType")] 
    public string CrustType { get; init; }
    [BsonElement("size")] 
    public string Size { get; init; }
    [BsonElement("price"), BsonRepresentation(BsonType.Decimal128),BsonDefaultValue(12.99)] 
    public decimal Price { get; init; }
    [BsonElement("toppings")] 
    public IEnumerable<string> Toppings { get; init; }
    [BsonElement("isGlutenFree")]
    public bool IsGlutenFree { get; set; }
    [BsonElement("isVegan")]
    public bool IsVegan { get; set; }
    [BsonElement("isVegetarian")]
    public bool IsVegetarian { get; set; }
    [BsonElement("quantity")]
    public int Quantity { get; set; }

    [BsonElement("createdAt")]
    [BsonIgnoreIfDefault, BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    [BsonElement("updatedAt")] 
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow; 
}