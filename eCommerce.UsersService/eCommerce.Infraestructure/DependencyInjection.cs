using eCommerce.Core.DTOs;
using eCommerce.Core.Interfaces.Repositories;
using eCommerce.Core.Interfaces.Services;
using eCommerce.Core.Options;
using eCommerce.Infraestructure.Mappers;
using eCommerce.Infraestructure.Repositories;
using eCommerce.Infraestructure.Services;
using eCommerce.Infraestructure.Validators;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace eCommerce.Infraestructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<IUsersRepository, UsersRepository>();
        services.AddTransient<IUsersService, UsersService>();
        services.AddTransient<JwtTokenGenerator>();
        services.Configure<JwtOption>(configuration.GetSection("Jwt"));
        services.AddAutoMapper(cfg =>
        {
            var licenceKey = configuration["MapperLicenseKey"];
            if (string.IsNullOrEmpty(licenceKey))
                throw new InvalidOperationException("Mapper license key is not configured.");

            cfg.LicenseKey = licenceKey;
            cfg.AddProfile(new ApplicationUserMapper());
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
        services.AddScoped<IValidator<LoginRequest>, LoginRequestValidator>();
        services.AddScoped<IValidator<RegisterRequest>, RegisterRequestValidator>();

        return services;
    }
}
