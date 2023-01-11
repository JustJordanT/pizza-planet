using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace PizzaPlanet.API.Entities;

public class CustomerEntity
{
    [BsonId, BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; init; }
    [BsonElement("name")] 
    public string Name { get; set; }
    [BsonElement("email")] 
    public string Email { get; set; }
    [BsonElement("password")] 
    public string Password { get; set; }
    [BsonElement("passwordHash")] 
    public string PasswordHash { get; set; }
    [BsonElement("passwordSalt")] 
    public string PasswordSalt { get; set; }
    [BsonElement("createdAt")] 
    public DateTime CreatedAt { get; init; } = DateTime.UtcNow;
    [BsonElement("updatedAt")] 
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}