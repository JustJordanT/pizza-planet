using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using PizzaPlanet.API.Commons;

namespace PizzaPlanet.API.Models;

public class CreatePizzaModel
{
    [RegularExpression(PropertyRegex.CrustType)]
    public string CrustType { get; init; }
    [RegularExpression(PropertyRegex.Size, ErrorMessage = "Size must be, L, M, or S")]
    public string Size { get; init; }

    public List<string> Toppings { get; init; }
    public bool IsGlutenFree { get; set; }
    public bool IsVegan { get; set; }
    public bool IsVegetarian { get; set; }
    public int Quantity { get; set; }
};