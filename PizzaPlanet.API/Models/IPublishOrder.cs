using System.Drawing.Printing;

namespace PizzaPlanet.Messages;

public class IPublishOrder 
{
    public string Id { get; set; }
    public List<string> PizzaIds { get; set;}
}