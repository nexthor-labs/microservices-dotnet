using System;
using AutoMapper;
using eCommerce.Core.DTOs;
using eCommerce.Core.Entities;

namespace eCommerce.Infraestructure.Mappers;

public class OrderItemsResponseMapper : Profile
{
    public OrderItemsResponseMapper()
    {
        CreateMap<OrderItemResponse, OrderItem>()
            .ReverseMap();
    }
}
