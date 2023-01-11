using System.ComponentModel.DataAnnotations;
using PizzaPlanet.API.Commons;

namespace PizzaPlanet.API.Models;

public class CreateCustomer
{
    [RegularExpression(PropertyRegex.name)]
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}