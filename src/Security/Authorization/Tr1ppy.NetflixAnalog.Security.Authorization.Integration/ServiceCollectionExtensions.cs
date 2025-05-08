using Microsoft.Extensions.DependencyInjection;

namespace Tr1ppy.NetflixAnalog.Security.Authorization.Integration;

using Authorization.Contracts;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDefaultAuthorization(this IServiceCollection services)
    {
        services.AddAuthorizationCore(options =>
        {
            options.AddPolicy(PolicyNames.BaseUser, policy =>
            {
                policy.RequireAuthenticatedUser();
                policy.RequireRole("Base");
            });

            options.AddPolicy(PolicyNames.OnlyAdmin, policy =>
            {
                policy.RequireAuthenticatedUser();
                policy.RequireRole("Admin");
            });
        });

        return services;
    }
}
