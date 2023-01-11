using System.ComponentModel.DataAnnotations;
using PizzaPlanet.API.Commons;

namespace PizzaPlanet.API.Models;

public class LoginCustomer
{
    public string Email { get; set; }
    // [RegularExpression(PropertyRegex.StrongPassword)]
    public string Password { get; set; } 
}