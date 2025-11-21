using System;

namespace eCommerce.Core.DTOs;

public class OrderUpdateRequest : OrderRequestBase
{
    public Guid OrderId { get; set; }
}
