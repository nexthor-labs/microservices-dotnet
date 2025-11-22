using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace eCommerce.Core.Entities;

public class OrderItem
{
    [BsonRepresentation(BsonType.String)]
    public Guid ProductId { get; set; }
    
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal TotalPrice { get; set; }
}
