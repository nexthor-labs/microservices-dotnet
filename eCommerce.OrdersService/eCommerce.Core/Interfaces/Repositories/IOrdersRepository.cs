using System;
using System.Linq.Expressions;
using eCommerce.Core.Entities;

namespace eCommerce.Core.Interfaces.Repositories;

public interface IOrdersRepository
{
    Task<ICollection<Order>> GetAllOrdersAsync(CancellationToken cancellationToken = default);
    Task<ICollection<Order>> GetOrdersByConditionAsync(Expression<Func<Order, bool>> predicate, CancellationToken cancellationToken = default);
    Task<Order?> GetOrderByConditionAsync(Expression<Func<Order, bool>> predicate, CancellationToken cancellationToken = default);
    Task<Order?> AddOrderAsync(Order order, CancellationToken cancellationToken = default);
    Task<Order?> UpdateOrderAsync(Order order, CancellationToken cancellationToken = default);
    Task DeleteOrderAsync(Guid orderId, CancellationToken cancellationToken = default);
}
