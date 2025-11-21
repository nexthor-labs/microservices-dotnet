using System;

namespace eCommerce.Core.Entities;

public class Order
{
    public Guid OrderId { get; set; }
    public string? UserId { get; set; }
    public DateTime OrderDate { get; set; }
    public decimal TotalAmount { get; set; }
    public ICollection<OrderItem> Items { get; set; } = [];
}
