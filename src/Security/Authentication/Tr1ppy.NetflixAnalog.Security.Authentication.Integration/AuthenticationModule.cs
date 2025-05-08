using Autofac;
using Autofac.Extensions.Services;
using Autofac.Extensions.MediatR;

using Microsoft.Extensions.Logging;

namespace Tr1ppy.NetflixAnalog.Security.Authentication.Integration;

using Tr1ppy.EntityFramework.Autofac;

using Authentication.Infrastructure;
using Authentication.DataAccess;
using Authentication.DataAccess.Repositories;
using Authentication.UseCases.Commands.Register;

public class AuthenticationModule(ILogger<AuthenticationModule> logger) : Autofac.Module
{
    private readonly ILogger<AuthenticationModule> _logger = logger
        ?? throw new ArgumentNullException(nameof(logger));

    protected override void Load(ContainerBuilder builder)
    {
        builder.AddMediatR(_logger, typeof(RegisterCommand).Assembly);

        builder.AddDataContext<AuthenticationDataContext>()
               .WithProviderFromConfiguration("DbProvider")
               .Register();

        builder.RegisterServicesWithAttributes(_logger, typeof(UserAccessor).Assembly)
               .RegisterServicesWithAttributes(_logger, typeof(UserRepository).Assembly);
    }
}
