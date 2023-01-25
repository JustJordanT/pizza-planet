using System.Reflection;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using PizzaPlanet.Kitchen.API.Consumers;
using PizzaPlanet.Kitchen.API.Context;
using PizzaPlanet.Kitchen.API.Services;
using PizzaPlanet.Kitchen.API.Services.Interfaces;
using RabbitMQ.Client;
using Serilog;
using Serilog.Core;
using ILogger = Microsoft.Extensions.Logging.ILogger;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<KitchenDbContext>(optionsBuilder =>
    optionsBuilder.UseNpgsql(@"Server=localhost;Port=5432;Database=postgres;User Id=postgres;Password=password123"));

// Repositories
builder.Services.AddScoped<ICooksRepository, CooksRepository>();
builder.Services.AddScoped<ICommonsRepository, CommonsRepository>();
builder.Services.AddScoped<IPizzasCompletedRepository, PizzasCompletedRepository>();
builder.Services.AddScoped<IOrdersCompletedRepository, OrdersCompletedRepository>();
builder.Services.AddScoped<IFireOven, FireOvenService>();

var serviceProvider = builder.Services.BuildServiceProvider();
var logger = serviceProvider.GetService<ILogger<KitchenConsumer>>();
builder.Services.AddSingleton(typeof(ILogger), logger);
var fireOven = serviceProvider.GetService<IFireOven>();

// MassTransit RabbitMQ Config

builder.Services.AddMassTransit(x =>
{
    // x.AddConsumer<KitchenConsumer>();
    x.SetKebabCaseEndpointNameFormatter();
    x.UsingRabbitMq((context, cfg) =>
    {
        // cfg.ConfigureEndpoints(context);
        // cfg.OverrideDefaultBusEndpointQueueName("order-consumer");
        cfg.ReceiveEndpoint("order-service", re =>
        {
            re.Consumer(() => new KitchenConsumer(logger, fireOven));
            
            // turns off default fanout settings
            re.ConfigureConsumeTopology = false;

            // a replicated queue to provide high availability and data safety. available in RMQ 3.8+
            re.SetQuorumQueue();

            // enables a lazy queue for more stable cluster with better predictive performance.
            // Please note that you should disable lazy queues if you require really high performance, if the queues are always short, or if you have set a max-length policy.
            re.SetQueueArgument("declare", "lazy");
            re.ExchangeType = ExchangeType.Direct;
        });
    });
    
});

// Serilog
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .WriteTo.File("logs/logs.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();
builder.Host.UseSerilog();
Log.Information("The global logger has been configured");

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
