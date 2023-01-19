using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PizzaPlanet.Kitchen.API.Entities;

namespace PizzaPlanet.Kitchen.API.Context;

public class KitchenDbContext : DbContext 
{
    public DbSet<CooksEntity> Cook { get; set; }
    public DbSet<Pizzas_Completed> PizzasCompleted { get; set; }
    public DbSet<Orders_Completed> OrdersCompleted { get; set; }
    
    
    public KitchenDbContext(DbContextOptions<KitchenDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Orders_Completed>()
            .HasOne(order => order.CookEntity)
            .WithMany(cook => cook.OrdersCompleted)
            .HasForeignKey(order => order.CookId);

        modelBuilder.Entity<Pizzas_Completed>()
            .HasOne(pizzas => pizzas.CookEntity)
            .WithMany(cook => cook.PizzasCompleted)
            .HasForeignKey(pizzas => pizzas.CookId);
    }
}