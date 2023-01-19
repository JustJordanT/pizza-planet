namespace PizzaPlanet.Kitchen.API.Services;

public interface ICommonsRepository
{
   Task SaveChangesAsync(CancellationToken cancellationToken);
}