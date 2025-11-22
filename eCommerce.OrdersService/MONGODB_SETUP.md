# MongoDB Setup Guide

## Overview
This microservice has been configured to use MongoDB as the database. The setup includes:

- MongoDB.Driver for database operations
- AppDbContext for managing collections
- MongoDB attributes for entity mapping

## Configuration

### 1. Connection String
Update `appsettings.json` or `appsettings.Development.json` with your MongoDB connection details:

```json
{
  "MongoDb": {
    "ConnectionString": "mongodb://localhost:27017",
    "DatabaseName": "eCommerceOrdersDb"
  }
}
```

For production, consider using MongoDB Atlas or secured connection strings:
```json
{
  "MongoDb": {
    "ConnectionString": "mongodb+srv://username:password@cluster.mongodb.net/",
    "DatabaseName": "eCommerceOrdersDb"
  }
}
```

### 2. Dependency Injection
MongoDB is registered in `Program.cs` using the extension method:

```csharp
builder.Services.AddMongoDb(builder.Configuration);
```

## Entity Configuration

Entities use MongoDB attributes for proper mapping:

```csharp
public class Order
{
    [BsonId]
    [BsonRepresentation(BsonType.String)]
    public Guid OrderId { get; set; }
    // ... other properties
}
```

- `[BsonId]` - Marks the property as the document's `_id` field
- `[BsonRepresentation(BsonType.String)]` - Stores Guid as string in MongoDB

## Using AppDbContext

Inject `AppDbContext` into your repositories or services:

```csharp
public class OrdersRepository : IOrdersRepository
{
    private readonly AppDbContext _context;

    public OrdersRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Order?> AddOrderAsync(Order order)
    {
        await _context.Orders.InsertOneAsync(order);
        return order;
    }
}
```

## Common MongoDB Operations

### Insert
```csharp
await _context.Orders.InsertOneAsync(order);
```

### Find All
```csharp
var orders = await _context.Orders.Find(_ => true).ToListAsync();
```

### Find by Filter
```csharp
var order = await _context.Orders
    .Find(o => o.OrderId == orderId)
    .FirstOrDefaultAsync();
```

### Update
```csharp
await _context.Orders.ReplaceOneAsync(
    o => o.OrderId == order.OrderId,
    order
);
```

### Delete
```csharp
await _context.Orders.DeleteOneAsync(o => o.OrderId == orderId);
```

## Running MongoDB Locally

### Using Docker:
```bash
docker run -d -p 27017:27017 --name mongodb mongo:latest
```

### Using Docker Compose:
Create a `docker-compose.yml`:
```yaml
version: '3.8'
services:
  mongodb:
    image: mongo:latest
    ports:
      - "27017:27017"
    volumes:
      - mongodb_data:/data/db

volumes:
  mongodb_data:
```

Run with:
```bash
docker-compose up -d
```

## Adding New Collections

To add a new collection to `AppDbContext`:

```csharp
public IMongoCollection<YourEntity> YourEntities => 
    _database.GetCollection<YourEntity>("yourentities");
```

## Package References

The following MongoDB packages are used (managed centrally in `Directory.Packages.props`):
- `MongoDB.Driver` - Full MongoDB driver
- `MongoDB.Bson` - BSON serialization and attributes
