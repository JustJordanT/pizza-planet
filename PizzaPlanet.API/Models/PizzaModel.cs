using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace PizzaPlanet.API.Models;

public record PizzaModel
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    [BsonElement("crustType")]
    public string CrustType { get; init; }
    [BsonElement("size")]
    public string Size { get; init; }
    [BsonElement("toppings")]
    public IEnumerable<string> Toppings { get; init; }
    [BsonElement("timeStamp")]
    public DateTime TimeStamp { get; set; }
};