using System;

namespace eCommerce.Core.DTOs;

public class OrderResponse
{
    public Guid OrderId { get; set; }
    public string? UserId { get; set; }
    public DateTime OrderDate { get; set; }
    public decimal TotalAmount { get; set; }
    public ICollection<OrderItemResponse> Items { get; set; } = [];
}
