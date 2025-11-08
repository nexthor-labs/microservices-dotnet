using eCommerce.Core.Interfaces.Repositories;
using eCommerce.Core.Interfaces.Services;
using eCommerce.Infraestructure.Repositories;
using eCommerce.Infraestructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace eCommerce.Infraestructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddTransient<IUsersRepository, UsersRepository>();
        services.AddTransient<IUsersService, UsersService>();

        return services;
    }
}
