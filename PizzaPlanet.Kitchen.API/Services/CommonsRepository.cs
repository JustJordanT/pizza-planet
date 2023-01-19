using PizzaPlanet.Kitchen.API.Context;

namespace PizzaPlanet.Kitchen.API.Services;

public class CommonsRepository : ICommonsRepository
{
    private readonly KitchenDbContext _kitchenDbContext;

    public CommonsRepository(KitchenDbContext kitchenDbContext)
    {
        _kitchenDbContext = kitchenDbContext;
    }
    
    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
       await _kitchenDbContext.SaveChangesAsync(cancellationToken);
    }
}