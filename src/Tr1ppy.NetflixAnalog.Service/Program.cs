using System.Reflection;

using Microsoft.Extensions.Diagnostics.Metrics;
using Microsoft.Extensions.Localization;

using NLog;
using NLog.Extensions.Logging;

using Autofac;
using Autofac.Extensions.Modules;
using Autofac.Extensions.Services;
using Autofac.Extensions.DependencyInjection;

namespace Tr1ppy.NetflixAnalog;

using Tr1ppy.Logging.Extensions;

using Service.Lexems;
using Service.Resourses;
using Service.Extensions;
using Tr1ppy.Cryptography.Password.Integration;
using System.Security.Cryptography;

public static class Program
{
    private static readonly Logger _logger =
        LogManager.Setup()
                  .LoadConfigurationFromFile("Settings/NLog.config")
                  .GetCurrentClassLogger();

    public static async Task Main(string[] args)
    {
        string startupDateTime = DateTime.Now.ToString("G");

        WebApplicationBuilder builder = ConfigureBuilder(args);
        WebApplication app = builder.Build();

        using var scope = app.Services.CreateScope();
        var localizer = scope.ServiceProvider.GetRequiredService<IStringLocalizer<SharedResourse>>();
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<SharedResourse>>();

        try
        {
            logger.LogInformation
            (
                localizer: localizer, 
                key: LoggingLexems.StartupTimeMessage, 
                args: [startupDateTime]
            );

            ConfigureApp(app);
            await app.RunAsync();
        }
        catch (Exception ex)
        {
            logger.LogError
            (
                localizer: localizer, 
                key: LoggingLexems.StartApplicationError, 
                exception: ex
            );
            throw;
        }
        finally
        {
            LogManager.Shutdown();
        }
    }

    #region Configuration

    private static void ConfigureApp
    (
        WebApplication app
    )
    {
        app.UseDeveloperExceptionPage();
        app.UseSwagger();
        app.UseSwaggerUI();

        app.UseRouting();
        app.UseCors();

        var appName = app.Configuration["ServiceName"]
            ?? throw new ArgumentNullException(null, "Service name not specified");

        app.MapGet(string.Empty, async ctx => await ctx.Response.WriteAsync(appName));

        app.UseAuthorization();
        app.MapControllers();
    }

    private static WebApplicationBuilder ConfigureBuilder
    (
        string[] args
    )
    {
        var builder = WebApplication.CreateBuilder(new WebApplicationOptions()
        {
            Args = args,
            ContentRootPath = Directory.GetCurrentDirectory()
        });

        builder.Configuration
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "Settings"))
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

        builder.Host
            .ConfigureMetrics(ConfigureMetrics)
            .ConfigureLogging(ConfigureLogging)
            .ConfigureServices(ConfigureServices)
            .ConfigureContainer<ContainerBuilder>(ConfigureContainer)
            .UseServiceProviderFactory(new AutofacServiceProviderFactory())
            .UseConsoleLifetime();

        return builder;
    }

    #region Host Configuration

    private static void ConfigureLogging    
    (
        HostBuilderContext context, 
        ILoggingBuilder loggingBuilder
    )
    {
        loggingBuilder.ClearProviders();

        loggingBuilder.AddNLog();
        _logger.Debug("Succesfully configured logging!");
    }

    private static void ConfigureMetrics
    (
        HostBuilderContext context, 
        IMetricsBuilder metricsBuilder
    )
    {
        _logger.Debug("Succesfully configured metrics!");
    }

    private static void ConfigureServices
    (
        HostBuilderContext context, 
        IServiceCollection services
    )
    {
        var configuration = context.Configuration;
        
        services.AddLocalizationSupport(context.Configuration);
        services.AddControllersWithCors();
        services.AddSwaggerDocumentation();

        IConfigurationSection securitySection = configuration.GetSection("Security");
        services.AddPasswordCryptography(options =>
        {
            options.SaltSize = securitySection.GetValue<int>("SaltSize");
            options.HashSize = securitySection.GetValue<int>("HashSize");
            options.HashAlgorithm = new HashAlgorithmName(securitySection.GetValue<string>("HashAlgorithm"));
            options.IterationsCount = securitySection.GetValue<int>("HashIterationsCount");
        });

        _logger.Debug("Succesfully configured services!");
    }

    private static void ConfigureContainer
    (
        ContainerBuilder containerBuilder
    )
    {
        containerBuilder.RegisterDomainsModules(opts =>
        {
            opts.IncludeCurrentDomainName('.');
            opts.IncludeDomainNames("Integration");
        });

        containerBuilder.RegisterServicesWithAttributes
        (
            logger: null,
            assemblies: Assembly.GetExecutingAssembly()
        );
    }

    #endregion

    #endregion
}