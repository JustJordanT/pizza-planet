using System.Reflection;
using FluentValidation.AspNetCore;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using PizzaPlanet.API.Context;
using PizzaPlanet.API.Services;
using PizzaPlanet.API.Services.Interfaces;
using PizzaPlanet.Contracts;
using RabbitMQ.Client;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog();

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
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.ConfigureEndpoints(context);
        cfg.Host("moose-01.rmq.cloudamqp.com", "ushbdexq", h =>
        {
            h.Username("ushbdexq");
            h.Password("w7IjHDIsCAtzl1swRjMPMNhs41P1YCbg");
        });
        cfg.Message<IPublishOrder>(e => e.SetEntityName("order-service"));
        cfg.Publish<IPublishOrder>(e => e.ExchangeType = ExchangeType.Direct);
        
        // Exchange/queue Configuration for order status  
        cfg.ReceiveEndpoint("order-update-service", re =>
        {
            // Update configuration for consumer for Getting orders
            // re.Consumer(() => new KitchenConsumer(logger, fireOven)); 
            
            // turns off default fanout settings
            re.ConfigureConsumeTopology = false;
            
            re.Bind("order-update-service-ex", b =>
            {
                b.ExchangeType = ExchangeType.Direct;
            });
            
            // a replicated queue to provide high availability and data safety. available in RMQ 3.8+
            re.SetQuorumQueue();

            // enables a lazy queue for more stable cluster with better predictive performance.
            // Please note that you should disable lazy queues if you require really high performance, if the queues are always short, or if you have set a max-length policy.
            re.SetQueueArgument("declare", "lazy");
            re.ExchangeType = ExchangeType.Direct;
        }); 
        
    });
});
builder.Services.AddMassTransitHostedService();

// Serilog
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .WriteTo.File("logs/logs.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();
Log.Information("The global logger has been configured");

// Ef Core Services
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