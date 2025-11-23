using System;
using AutoMapper;
using eCommerce.Core.DTOs;
using eCommerce.Core.Entities;

namespace eCommerce.Infraestructure.Mappers;

public class OrdersRequestMapper : Profile
{
    public OrdersRequestMapper()
    {
        CreateMap<OrderAddRequest, Order>()
            .ReverseMap();
        
        CreateMap<OrderUpdateRequest, Order>()
            .ReverseMap();
    }
}
