using System.ComponentModel;
using System.Reflection;
using FluentValidation;
using FluentValidation.AspNetCore;
using MassTransit;
using Microsoft.EntityFrameworkCore;
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

// MassTransit + RabbitMQ

builder.Services.AddMassTransit(x =>
{
    x.UsingRabbitMq((context,cfg) =>
    {
        cfg.Host("localhost", "/", h => {
            h.Username("guest");
            h.Password("guest");
        });

        cfg.ConfigureEndpoints(context);
        cfg.ReceiveEndpoint("published-orders", e =>
        {
            // e.ConfigureConsumer<MassApiConddsumer>(context);
        }) ;
    });
    // x.AddConsumer<SubmitOrderConsumer>(typeof(SubmitOrderConsumerDefinition));

    x.SetKebabCaseEndpointNameFormatter();

    // x.UsingRabbitMq((context, cfg) => cfg.ConfigureEndpoints(context));
});


// Add the MongoDbContext to the DI container
// builder.Services.AddSingleton<MongoDbContext>(new MongoDbContext(
//     builder.Configuration["MongoDatabase:ConnectionString"],
//     builder.Configuration["MongoDatabase:DatabaseName"]));

builder.Services.AddDbContext<PgSqlContext>(optionsBuilder =>
    optionsBuilder.UseNpgsql(@"Server=localhost;Port=5432;Database=postgres;User Id=postgres;Password=password123"));

// Repository Pattern
// builder.Services.AddTransient<IPizzaRepository, PizzasRepository>();
// builder.Services.AddTransient<ICustomerRepository, CustomerRepository>();
// builder.Services.AddTransient<IAuthenticationRepository, AuthenticationRepository>();
// builder.Services.AddTransient<ICartRepository, CartRepository>();

builder.Services.AddScoped<IPizzaRepository, PizzasRepository>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IAuthenticationRepository, AuthenticationRepository>();
builder.Services.AddScoped<ICartRepository, CartRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();

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