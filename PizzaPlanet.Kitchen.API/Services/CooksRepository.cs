using Microsoft.EntityFrameworkCore;
using PizzaPlanet.Kitchen.API.Commons.Mapper;
using PizzaPlanet.Kitchen.API.Context;
using PizzaPlanet.Kitchen.API.Entities;
using PizzaPlanet.Kitchen.API.Models;

namespace PizzaPlanet.Kitchen.API.Services.Interfaces;

public class CooksRepository : ICooksRepository
{
    private readonly KitchenDbContext kitchenDbContext;
    private readonly ICommonsRepository _commonsRepository;

    public CooksRepository(
        KitchenDbContext kitchenDbContext,
        ICommonsRepository commonsRepository)
    {
        this.kitchenDbContext = kitchenDbContext ?? throw new ArgumentNullException(nameof(kitchenDbContext));
        _commonsRepository = commonsRepository;
    }

    public async Task<CooksEntity> CreateCookAsync(CreateCookModel createCook, CancellationToken cancellationToken)
    {
        var cook = await kitchenDbContext.Cook.AddAsync(CookMapper.CreateCook(createCook), cancellationToken);
        await _commonsRepository.SaveChangesAsync(cancellationToken);
        return cook.Entity;
    }

    public async Task<bool> CooksExistAsync()
    {
         return 1 < await kitchenDbContext.Cook.Select(c => c.Id).CountAsync();
    }

    public async Task<string> GetRandomCookId()
    {
        if (!await CooksExistAsync())
        {
            throw new InvalidOperationException("No Cooks are present");
        }
        var cookIds = await kitchenDbContext.Cook.Select(c => c.Id).ToListAsync();
        var rnd = new Random();
        var index = rnd.Next(cookIds.Count);
        var randomCook = cookIds[index];
        return randomCook;
    }
}
