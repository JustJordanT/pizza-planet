An entity in Entity Framework (EF) is a class that represents a database table or view, and is used to query and manipulate data in the database. In the context of a pizza store, a Pizza entity might be used to represent the data for a single pizza in the database, including its size, crust type, and toppings.

Here is an example of a Pizza entity in C# for use with EF Core:

```csharp
public class Pizza
{
public int Id { get; set; }
public string Size { get; set; }
public string CrustType { get; set; }
public List<Topping> Toppings { get; set; }
}
```
This class has four properties: an Id (which is the primary key for the table), Size, CrustType, and a list of Toppings. It would be included in a DbContext class, which is used to manage the entities in the database and perform CRUD (create, read, update, delete) operations.

To use this entity with EF Core, you would need to configure the DbContext to include a DbSet for the Pizza entity, and use the DbContext to query and manipulate the data in the database. For example:

```csharp
public class PizzaStoreDbContext : DbContext
{
public DbSet<Pizza> Pizzas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("connection string goes here");
    }
}
```

With this setup, you can use the PizzaStoreDbContext to query and manipulate the data in the Pizzas table in the database.