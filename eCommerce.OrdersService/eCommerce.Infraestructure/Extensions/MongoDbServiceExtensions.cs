using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using eCommerce.Core.Options;

namespace eCommerce.Infraestructure.Extensions;

public static class MongoDbServiceExtensions
{
    public static IServiceCollection AddMongoDb(this IServiceCollection services, IConfiguration configuration)
    {
        // Bind MongoDB configuration
        var mongoDbOptions = configuration.GetSection("MongoDb").Get<MongoDbOptions>();
        
        if (mongoDbOptions == null || string.IsNullOrEmpty(mongoDbOptions.ConnectionString))
        {
            throw new InvalidOperationException("MongoDB configuration is missing or invalid.");
        }

        services.Configure<MongoDbOptions>(configuration.GetSection("MongoDb"));

        // Register MongoDB client as singleton
        services.AddSingleton<IMongoClient>(sp =>
        {
            return new MongoClient(mongoDbOptions.ConnectionString);
        });

        // Register AppDbContext
        services.AddScoped<AppDbContext>(sp =>
        {
            var mongoClient = sp.GetRequiredService<IMongoClient>();
            return new AppDbContext(mongoClient, mongoDbOptions.DatabaseName);
        });

        return services;
    }
}
