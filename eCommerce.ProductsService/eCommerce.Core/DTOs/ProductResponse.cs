using System;

namespace eCommerce.Core.DTOs;

public class ProductResponse
{
    public Guid ProductID { get; set; } = Guid.NewGuid();
    public string? ProductName { get; set; }
    public string? Category { get; set; }
    public decimal UnitPrice { get; set; }
    public int QuantityInStock { get; set; }
}
