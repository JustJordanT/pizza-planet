using Microsoft.AspNetCore.Mvc;
using PizzaPlanet.API.Models;

namespace PizzaPlanet.API.Controllers;

[ApiController]
[Route("api/order")]
public class OrderPizzaController : ControllerBase
{
    // List<Pizzas> orders = new List<Pizzas>();
    
    [HttpGet]
    public  ActionResult Get([FromQuery] int count)
    {
        return  Ok(OrdersDataStore.Current.Orders.Take(count));
    }

    [HttpPost]
    public ActionResult Post([FromBody] Pizzas orderPizza)
    {
        OrdersDataStore.Current.Orders.Add(orderPizza);
        return Created("", orderPizza);
    }

    [HttpDelete]
    public ActionResult Delete(int orderId)
    {
        var order = OrdersDataStore.Current.Orders
            .FirstOrDefault(x => x.Id == orderId);
        if (order == null)
            return NotFound();
        
        OrdersDataStore.Current.Orders.Remove(order);
        return NoContent();
    }
}