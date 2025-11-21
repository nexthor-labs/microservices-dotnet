using System;

namespace eCommerce.Core.DTOs;

public class OrderRequestBase
{
    public string? UserId { get; set; }
    public ICollection<OrderItemRequest> Items { get; set; } = [];
    public DateTime OrderDate { get; set; } = DateTime.UtcNow;
}
