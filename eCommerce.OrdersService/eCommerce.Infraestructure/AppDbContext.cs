using MongoDB.Driver;
using eCommerce.Core.Entities;

namespace eCommerce.Infraestructure;

public class AppDbContext
{
    private readonly IMongoDatabase _database;

    public AppDbContext(IMongoClient mongoClient, string databaseName)
    {
        _database = mongoClient.GetDatabase(databaseName);
    }

    public IMongoCollection<Order> Orders => _database.GetCollection<Order>("orders");
    
    // Add other collections as needed
    // public IMongoCollection<OtherEntity> OtherEntities => _database.GetCollection<OtherEntity>("otherentities");
}
