using System;
using System.Linq.Expressions;
using eCommerce.Core.Entities;
using eCommerce.Core.Interfaces.Repositories;
using MongoDB.Driver;

namespace eCommerce.Infraestructure.Repositories;

public class OrdersRepository : IOrdersRepository
{
    private readonly AppDbContext _context;

    public OrdersRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Order?> AddOrderAsync(Order order, CancellationToken cancellationToken = default)
    {
        await _context.Orders.InsertOneAsync(order, cancellationToken: cancellationToken);
        return order;
    }

    public async Task DeleteOrderAsync(Guid orderId, CancellationToken cancellationToken = default)
    {
        await _context.Orders.DeleteOneAsync(o => o.OrderId == orderId, cancellationToken);
    }

    public async Task<ICollection<Order>> GetAllOrdersAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Orders.Find(_ => true).ToListAsync(cancellationToken);
    }

    public async Task<Order?> GetOrderByConditionAsync(Expression<Func<Order, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return await _context.Orders.Find(predicate).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<ICollection<Order>> GetOrdersByConditionAsync(Expression<Func<Order, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return await _context.Orders.Find(predicate).ToListAsync(cancellationToken);
    }

    public async Task<Order?> UpdateOrderAsync(Order order, CancellationToken cancellationToken = default)
    {
        var result = await _context.Orders.ReplaceOneAsync(
            o => o.OrderId == order.OrderId,
            order,
            cancellationToken: cancellationToken
        );
        
        return result.ModifiedCount > 0 ? order : null;
    }
}
