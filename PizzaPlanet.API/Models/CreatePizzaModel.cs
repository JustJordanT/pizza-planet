using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using PizzaPlanet.API.Commons;

namespace PizzaPlanet.API.Models;

public class CreatePizzaModel
{
    [RegularExpression(PropertyRegex.CrustType)]
    public string CrustType { get; set; }
    [RegularExpression(PropertyRegex.Size, ErrorMessage = "Size must be, L, M, or S")]
    public string Size { get; set; }

    public List<string> Toppings { get; set; }
    public bool IsGlutenFree { get; set; }
    public bool IsVegan { get; set; }
    public bool IsVegetarian { get; set; }
    public int Quantity { get; set; }
};