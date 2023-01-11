using System.Reflection;
using FluentValidation;
using FluentValidation.AspNetCore;
using PizzaPlanet.API.Commons.Validators;
using PizzaPlanet.API.Context;
using PizzaPlanet.API.Models;
using PizzaPlanet.API.Services;
using PizzaPlanet.API.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddFluentValidation(c => c.RegisterValidatorsFromAssemblies(new[] { Assembly.GetExecutingAssembly() }));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGenNewtonsoftSupport();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowOrigin",
        builder => builder
            .SetIsOriginAllowedToAllowWildcardSubdomains()
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader()
            .Build()
        );
});

// Add the MongoDbContext to the DI container
builder.Services.AddSingleton<MongoDbContext>(new MongoDbContext(
    builder.Configuration["MongoDatabase:ConnectionString"],
    builder.Configuration["MongoDatabase:DatabaseName"]));

// Repository Pattern
builder.Services.AddSingleton<IPizzaRepository, PizzasRepository>();
builder.Services.AddSingleton<ICustomerRepository, CustomerRepository>();
builder.Services.AddSingleton<IAuthenticationRepository, AuthenticationRepository>();

// Validators
// builder.Services.AddFluentValidation(c => c.RegisterValidatorsFromAssemblies(Assembly.GetExecutingAssembly()));
// builder.Services.AddValidatorsFromAssemblyContaining<IAssemblyMakers>();
// builder.Services.AddScoped<IValidator<PutPizzaModel>, PutRequestValidation>();

var app = builder.Build();

// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
app.UseSwagger();
app.UseSwaggerUI();

app.UseCors("AllowOrigin");
// app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();