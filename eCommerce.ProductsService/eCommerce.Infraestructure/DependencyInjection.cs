using System;
using eCommerce.Core.Options;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using eCommerce.Infraestructure.Repositories;
using eCommerce.Core.Interfaces.Repositories;
using eCommerce.Infraestructure.Mappers;
using FluentValidation;
using eCommerce.Core.DTOs;
using eCommerce.Infraestructure.Validators;

namespace eCommerce.Infraestructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IProductsRepository, ProductsRepository>();
        services.Configure<JwtOption>(configuration.GetSection("Jwt"));
        services.AddAutoMapper(cfg =>
        {
            var licenceKey = configuration["MapperLicenseKey"];
            if (string.IsNullOrEmpty(licenceKey))
                throw new InvalidOperationException("Mapper license key is not configured.");

            cfg.LicenseKey = licenceKey;
            cfg.AddProfile(new ProductMapper());
        });

        return services;
    }

    public static IServiceCollection AddInfrastructureDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("eCommerceDbContext");
        if (string.IsNullOrEmpty(connectionString))
            throw new InvalidOperationException("Database connection string is not configured.");

        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        });

        return services;
    }

    public static IServiceCollection AddInfrastructureValidators(this IServiceCollection services)
    {
        services.AddScoped<IValidator<ProductRequest>, ProductRequestValidator>();
        services.AddScoped<IValidator<ProductUpdateStockRequest>, ProductUpdateStockValidator>();

        return services;
    }
}
