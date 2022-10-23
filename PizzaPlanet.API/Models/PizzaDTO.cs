namespace PizzaPlanet.API.Models;

public record Pizzas
{
    public int Id { get; init; }
    public string Name { get; init; }
    public string Crust { get; init; }
    public IEnumerable<string> Toppings { get; init; }
    public string Size { get; init; }
    public DateTime CreatedAt { get; init; }
}