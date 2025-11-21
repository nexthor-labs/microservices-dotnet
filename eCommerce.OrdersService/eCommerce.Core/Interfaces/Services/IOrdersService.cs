using System;
using eCommerce.Core.DTOs;

namespace eCommerce.Core.Interfaces.Services;

public interface IOrdersService
{
    Task<ICollection<OrderResponse>> GetAllOrdersAsync(CancellationToken cancellationToken = default);
    Task<ICollection<OrderResponse>> GetOrdersByUserIdAsync(string userId, CancellationToken cancellationToken = default);
    Task<OrderResponse?> GetOrderByIdAsync(Guid orderId, CancellationToken cancellationToken = default);
    Task<OrderResponse?> GetOrderByProductAsync(Guid productId, CancellationToken cancellationToken = default);
    Task<OrderResponse?> AddOrderAsync(OrderAddRequest orderRequest, CancellationToken cancellationToken = default);
    Task<OrderResponse?> UpdateOrderAsync(OrderUpdateRequest orderRequest, CancellationToken cancellationToken = default);
    Task DeleteOrderAsync(Guid orderId, CancellationToken cancellationToken = default);
}
