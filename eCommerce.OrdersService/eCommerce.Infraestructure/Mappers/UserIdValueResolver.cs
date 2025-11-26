using System;
using System.Security.Claims;
using AutoMapper;
using eCommerce.Core.DTOs;
using eCommerce.Core.Entities;
using Microsoft.AspNetCore.Http;

namespace eCommerce.Infraestructure.Mappers;

public class UserIdValueResolver : IValueResolver<OrderAddRequest, Order, string?>
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserIdValueResolver(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string? Resolve(OrderAddRequest source, Order destination, string? destMember, ResolutionContext context)
    {
        var user = _httpContextAccessor.HttpContext?.User;
        
        if (user?.Identity?.IsAuthenticated == true)
        {
            // Try to get the user ID from different possible claim types
            var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value
                      ?? user.FindFirst("sub")?.Value
                      ?? user.FindFirst("userId")?.Value
                      ?? user.FindFirst("id")?.Value;
            
            return userId;
        }

        return null;
    }
}
