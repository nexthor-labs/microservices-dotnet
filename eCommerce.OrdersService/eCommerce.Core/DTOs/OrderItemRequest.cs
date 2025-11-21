using System;

namespace eCommerce.Core.DTOs;

public class OrderItemRequest
{
    public Guid ProductId { get; set; }
    public decimal UnitPrice { get; set; }
    public int Quantity { get; set; }
}
