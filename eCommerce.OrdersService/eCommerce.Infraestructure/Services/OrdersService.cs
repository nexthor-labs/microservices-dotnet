using System;
using AutoMapper;
using eCommerce.Core.DTOs;
using eCommerce.Core.Entities;
using eCommerce.Core.Interfaces.Repositories;
using eCommerce.Core.Interfaces.Services;
using FluentValidation;

namespace eCommerce.Infraestructure.Services;

public class OrdersService : IOrdersService
{
    private readonly IOrdersRepository _repository;
    private readonly IMapper _mapper;
    private readonly IValidator<OrderAddRequest> _orderAddValidator;
    private readonly IValidator<OrderUpdateRequest> _orderUpdateValidator;
    private readonly IValidator<OrderItemRequest> _orderItemValidator;

    public OrdersService(IOrdersRepository repository, 
    IMapper mapper,
    IValidator<OrderAddRequest> orderAddValidator,
    IValidator<OrderUpdateRequest> orderUpdateValidator,
    IValidator<OrderItemRequest> orderItemValidator)
    {
        _repository = repository;
        _mapper = mapper;
        _orderAddValidator = orderAddValidator;
        _orderUpdateValidator = orderUpdateValidator;
        _orderItemValidator = orderItemValidator;
    }

    public async Task<OrderResponse?> AddOrderAsync(OrderAddRequest request, CancellationToken cancellationToken = default)
    {
        var validate = await _orderAddValidator.ValidateAsync(request);
        if (!validate.IsValid)
            throw new ValidationException(validate.Errors);

       var orderEntity = _mapper.Map<Order>(request);
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

    public async Task<OrderResponse?> UpdateOrderAsync(OrderUpdateRequest request, CancellationToken cancellationToken = default)
    {
        var validate = await _orderUpdateValidator.ValidateAsync(request);
        if (!validate.IsValid)
            throw new ValidationException(validate.Errors);

       var order = await _repository.GetOrderByConditionAsync(o => o.OrderId == request.OrderId, cancellationToken);
        if (order != null)
        {
            _mapper.Map(request, order);
            var updatedOrder = await _repository.UpdateOrderAsync(order, cancellationToken);
            return _mapper.Map<OrderResponse?>(updatedOrder);
        }

        return null;
    }
}
