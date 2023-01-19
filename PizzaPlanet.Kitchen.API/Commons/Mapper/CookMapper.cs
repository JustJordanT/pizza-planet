using PizzaPlanet.Kitchen.API.Entities;
using PizzaPlanet.Kitchen.API.Models;

namespace PizzaPlanet.Kitchen.API.Commons.Mapper;

public static class CookMapper
{
    public static CooksEntity CreateCook(CreateCookModel createCook)
    {
        var cook = new CooksEntity
        {
            Name = createCook.Name,
            IsAvailable = createCook.IsAvailable,
        };

        return cook;
    }
}