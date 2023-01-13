using PizzaPlanet.API.Entities;

namespace PizzaPlanet.API.Models;

public class PutCartModel
{
    public string Id { get; set; }
    public List<string> PizzasEntities { get; set; }
    public string CustomerId { get; set; }
    public bool IsActive { get; set; }
}