using eCommerce.Core.Interfaces.Repositories;
using eCommerce.Core.Interfaces.Services;
using eCommerce.Infraestructure.Mappers;
using eCommerce.Infraestructure.Repositories;
using eCommerce.Infraestructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace eCommerce.Infraestructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<IUsersRepository, UsersRepository>();
        services.AddTransient<IUsersService, UsersService>();
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
}
