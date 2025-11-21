using System;

namespace eCommerce.Core.DTOs;

public class OrderItemResponse
{
    public Guid ProductId { get; set; }
    public decimal UnitPrice { get; set; }
    public int Quantity { get; set; }
    public decimal TotalPrice { get; set; }
}
