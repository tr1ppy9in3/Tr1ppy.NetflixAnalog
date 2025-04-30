using Microsoft.Extensions.Diagnostics.Metrics;

namespace Tr1ppy.NetflixAnalog;

public class Program
{
    public static async Task Main(string[] args)
    {

    }

    private static WebApplicationBuilder ConfigureApp(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Host.ConfigureServices(ConfigureServices)
                    .ConfigureMetrics(ConfigureMetrics)
                    .ConfigureLogging(ConfigureLogging)
                    .UseConsoleLifetime()
                    .

    }

    private static void ConfigureHostConfiguration
    (
        HostBuilderContext context, 
        IConfigurationBuilder configurationBuilder
    )
    {

    }

    private static void ConfigureAppConfiguration
    (
        HostBuilderContext context, 
        IConfigurationBuilder configurationBuilder
    )
    {

    }

    private static void ConfigureLogging
    (
        HostBuilderContext context, 
        ILoggingBuilder loggingBuilder
    )
    {
        loggingBuilder.ClearProviders();
        loggingBuilder.AddNLog


    }

    private static void ConfigureMetrics
    (
        HostBuilderContext context, 
        IMetricsBuilder metricsBuilder
    )
    {

    }

    private static void ConfigureServices
    (
        HostBuilderContext context, 
        IServiceCollection services
    )
    {

    }
}