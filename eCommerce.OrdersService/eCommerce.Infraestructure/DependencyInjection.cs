using System;
using eCommerce.Core.DTOs;
using eCommerce.Core.Interfaces.Repositories;
using eCommerce.Core.Interfaces.Services;
using eCommerce.Core.Options;
using eCommerce.Infraestructure.Mappers;
using eCommerce.Infraestructure.Repositories;
using eCommerce.Infraestructure.Services;
using eCommerce.Infraestructure.Validators;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace eCommerce.Infraestructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpContextAccessor();
        services.AddScoped<IOrdersRepository, OrdersRepository>();
        services.AddScoped<IOrdersService, OrdersService>();
        services.Configure<JwtOption>(configuration.GetSection("Jwt"));
        services.AddAutoMapper(cfg =>
        {
            var licenceKey = configuration["MapperLicenseKey"];
            if (string.IsNullOrEmpty(licenceKey))
                throw new InvalidOperationException("Mapper license key is not configured.");

            cfg.LicenseKey = licenceKey;
            cfg.AddProfile(new OrderItemsRequestMapper());
            cfg.AddProfile(new OrderItemsResponseMapper());
            cfg.AddProfile(new OrdersResponseMapper());
            cfg.AddProfile(new OrdersRequestMapper());
        });
        return services;
    }

    public static IServiceCollection AddInfrastructureValidators(this IServiceCollection services)
    {
        services.AddScoped<IValidator<OrderAddRequest>, OrderAddRequestValidator>();
        services.AddScoped<IValidator<OrderItemRequest>, OrderItemRequestValidator>();
        services.AddScoped<IValidator<OrderUpdateRequest>, OrderUpdateRequestValidator>();

        return services;
    }
}
