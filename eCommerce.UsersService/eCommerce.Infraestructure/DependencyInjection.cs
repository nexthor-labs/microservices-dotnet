using eCommerce.Core.Interfaces.Repositories;
using eCommerce.Infraestructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace eCommerce.Infraestructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddTransient<IUsersRepository, UsersRepository>();

        return services;
    }
}
