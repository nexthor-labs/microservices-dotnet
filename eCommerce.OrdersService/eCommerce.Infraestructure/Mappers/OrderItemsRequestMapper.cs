using System;
using AutoMapper;
using eCommerce.Core.DTOs;
using eCommerce.Core.Entities;

namespace eCommerce.Infraestructure.Mappers;

public class OrderItemsRequestMapper : Profile
{
    public OrderItemsRequestMapper()
    {
        CreateMap<OrderItemRequest, OrderItem>()
            .ReverseMap();
    }
}
