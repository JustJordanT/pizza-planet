using PizzaPlanet.API.Context;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
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