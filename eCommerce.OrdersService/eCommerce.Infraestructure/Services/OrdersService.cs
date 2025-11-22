using System;
using AutoMapper;
using eCommerce.Core.DTOs;
using eCommerce.Core.Entities;
using eCommerce.Core.Interfaces.Repositories;
using eCommerce.Core.Interfaces.Services;

namespace eCommerce.Infraestructure.Services;

public class OrdersService : IOrdersService
{
    private readonly IOrdersRepository _repository;
    private readonly IMapper _mapper;

    public OrdersService(IOrdersRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<OrderResponse?> AddOrderAsync(OrderAddRequest orderRequest, CancellationToken cancellationToken = default)
    {
        var orderEntity = _mapper.Map<Order>(orderRequest);
        var order = await _repository.AddOrderAsync(orderEntity, cancellationToken);

        return _mapper.Map<OrderResponse?>(order);
    }

    public async Task DeleteOrderAsync(Guid orderId, CancellationToken cancellationToken = default)
    {
        var order = await _repository.GetOrderByConditionAsync(o => o.OrderId == orderId, cancellationToken);
        if (order != null)
        {
            await _repository.DeleteOrderAsync(orderId, cancellationToken);
        }
    }

    public async Task<ICollection<OrderResponse>> GetAllOrdersAsync(CancellationToken cancellationToken = default)
    {
        var orders = await _repository.GetAllOrdersAsync(cancellationToken);

        return _mapper.Map<ICollection<OrderResponse>>(orders);
    }

    public async Task<OrderResponse?> GetOrderByIdAsync(Guid orderId, CancellationToken cancellationToken = default)
    {
        var order = await _repository.GetOrderByConditionAsync(o => o.OrderId == orderId, cancellationToken);
        return _mapper.Map<OrderResponse?>(order);
    }

    public async Task<OrderResponse?> GetOrderByProductAsync(Guid productId, CancellationToken cancellationToken = default)
    {
        var order = await _repository.GetOrderByConditionAsync(
            o => o.Items.Any(item => item.ProductId == productId),
            cancellationToken
        );
        return _mapper.Map<OrderResponse?>(order);
    }

    public async Task<ICollection<OrderResponse>> GetOrdersByUserIdAsync(string userId, CancellationToken cancellationToken = default)
    {
        var orders = await _repository.GetOrdersByConditionAsync(o => o.UserId == userId, cancellationToken);
        return _mapper.Map<ICollection<OrderResponse>>(orders);
    }

    public async Task<OrderResponse?> UpdateOrderAsync(OrderUpdateRequest orderRequest, CancellationToken cancellationToken = default)
    {
        var order = await _repository.GetOrderByConditionAsync(o => o.OrderId == orderRequest.OrderId, cancellationToken);
        if (order != null)
        {
            _mapper.Map(orderRequest, order);
            var updatedOrder = await _repository.UpdateOrderAsync(order, cancellationToken);
            return _mapper.Map<OrderResponse?>(updatedOrder);
        }

        return null;
    }
}
