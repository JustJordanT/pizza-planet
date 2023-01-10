using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace PizzaPlanet.API.Models;


public record PutPizzaModel
{
    // [Required(ErrorMessage = "Id is present, please provide a valid id")]
    public string Id { get; set; }
    public string CrustType { get; init; }
    public string Size { get; init; }
    public decimal Price { get; init; }
    public IEnumerable<string> Toppings { get; init; }
    public bool IsGlutenFree { get; set; }
    public bool IsVegan { get; set; }
    public bool IsVegetarian { get; set; }
    public int Quantity { get; set; }
};