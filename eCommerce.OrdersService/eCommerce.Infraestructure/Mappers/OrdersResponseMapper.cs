using System;
using AutoMapper;
using eCommerce.Core.DTOs;
using eCommerce.Core.Entities;

namespace eCommerce.Infraestructure.Mappers;

public class OrdersResponseMapper : Profile
{
    public OrdersResponseMapper()
    {
        CreateMap<OrderResponse, Order>()
            .ReverseMap();
    }
}
