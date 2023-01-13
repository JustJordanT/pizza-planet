using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using PizzaPlanet.API.Entities;

namespace PizzaPlanet.API.Models;

public class CreateCartModel
{
    public List<string> PizzasEntities { get; set; }
    public string CustomerId { get; set; }
}