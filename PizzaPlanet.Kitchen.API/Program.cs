using Microsoft.EntityFrameworkCore;
using PizzaPlanet.Kitchen.API.Context;
using PizzaPlanet.Kitchen.API.Services;
using PizzaPlanet.Kitchen.API.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<KitchenDbContext>(optionsBuilder =>
    optionsBuilder.UseNpgsql(@"Server=localhost;Port=5432;Database=postgres;User Id=postgres;Password=password123"));

builder.Services.AddScoped<ICooksRepository, CooksRepository>();
builder.Services.AddScoped<ICommonsRepository, CommonsRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();