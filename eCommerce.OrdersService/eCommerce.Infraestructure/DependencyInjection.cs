using System;
using eCommerce.Core;
using eCommerce.Core.DTOs;
using eCommerce.Core.Interfaces.Repositories;
using eCommerce.Core.Interfaces.Services;
using eCommerce.Core.Options;
using eCommerce.Infraestructure.Handlers;
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
        services.AddScoped<IProductsService, ProductsService>();
        services.Configure<JwtOption>(configuration.GetSection("Jwt"));
        services.Configure<HttpClientOption>(configuration.GetSection(OrdersServiceConstants.HttpClients).GetSection(OrdersServiceConstants.ProductsHttpClient));
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

    public static IServiceCollection AddNamedHttpClients(this IServiceCollection services)
    {
        services.AddTransient<BearerTokenDelegatingHandler>();
        
        services.AddHttpClient(OrdersServiceConstants.ProductsHttpClient, (sp, client) =>
        {
            var httpClientOption = sp.GetRequiredService<IConfiguration>()
                .GetSection(OrdersServiceConstants.HttpClients)
                .GetSection(OrdersServiceConstants.ProductsHttpClient)
                .Get<HttpClientOption>() ?? throw new InvalidOperationException("HTTP Client configuration is missing.");

            if (string.IsNullOrEmpty(httpClientOption.BaseAddress))
                throw new InvalidOperationException("Products HTTP Client BaseAddress is not configured.");

            client.BaseAddress = new Uri(httpClientOption.BaseAddress);
            // Additional client configuration can be done here (e.g., default headers, timeouts, etc.)
        })
        .AddHttpMessageHandler<BearerTokenDelegatingHandler>();

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
