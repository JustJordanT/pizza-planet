// using MongoDB.Bson.Serialization;
// using MongoDB.Bson.Serialization.Conventions;
// using MongoDB.Driver;
// using Microsoft.Extensions.Configuration;
// using Microsoft.Extensions.DependencyInjection;
// using Microsoft.Extensions.Options;
// using PizzaPlanet.API.Models;
//
// namespace PizzaPlanet.API.Services;
//
// public static class ServiceCollectionExtentions
// {
//     public static IServiceCollection AddMongoDb(this IServiceCollection services, IConfiguration configuration)
//     {
//         services.Configure<MongoDatabaseOptions>(configuration.GetSection("MongoDatabase"));
//         services.AddSingleton<IMongoClient>(provider => new MongoClient(configuration["MongoDatabase:ConnectionString"]));
//
//         var pack = new ConventionPack();
//         pack.Add(new CamelCaseElementNameConvention());
//         
//         ConventionRegistry.Register("Custom Conventions", pack, type => true);
//         BsonClassMap.RegisterClassMap<PizzaModel>(cm => cm.AutoMap());
//         return services;
//     }
// }