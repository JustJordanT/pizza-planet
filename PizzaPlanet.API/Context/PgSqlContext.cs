using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using PizzaPlanet.API.Entities;
using PizzaPlanet.API.Models;

namespace PizzaPlanet.API.Context;

public class PgSqlContext : DbContext
{
    public DbSet<PizzasEntity> PizzasEntity { get; set; } = null!;
    public DbSet<CustomerEntity> CustomerEntity { get; set; } = null!;
    public DbSet<CartEntity> CartEntity { get; set; } = null!;
    public DbSet<OrderEntity> OrderEntity { get; set; } = null!;

    // This is how we are able to set our DB settings from the program.cs file via the builder services.
    public PgSqlContext(DbContextOptions<PgSqlContext> options) : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // modelBuilder.Entity<PizzasEntity>()
        //     .HasOne(p => p.CartEntity)
        //     .WithMany(c => c.PizzasEntities)
        //     .HasForeignKey(p => p.CartId);

        modelBuilder.Entity<CartEntity>()
            .HasOne(cart => cart.Customer)
            .WithMany(customer => customer.Carts)
            .HasForeignKey(cart => cart.CustomerId);

        modelBuilder.Entity<OrderEntity>()
            .HasOne(order => order.Cart)
            .WithOne(cart => cart.Order)
            .HasForeignKey<OrderEntity>(order => order.CartId);
    }


}