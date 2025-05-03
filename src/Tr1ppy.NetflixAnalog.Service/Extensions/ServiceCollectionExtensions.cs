using Microsoft.AspNetCore.Localization;
using System.Globalization;
using System.Reflection;

namespace Tr1ppy.NetflixAnalog.Service.Extensions;

public static class ServiceCollectionSwagerExtension
{
    public static IServiceCollection AddControllersWithCors(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddCors(options =>
        {
            options.AddDefaultPolicy(policy =>
            {
                policy.AllowAnyHeader()
                      .AllowCredentials()
                      .AllowAnyMethod();
            });
        });

        return services;
    }

    public static IServiceCollection AddLocalizationSupport
    (
        this IServiceCollection services, 
        IConfiguration configuration
    )
    {
        IConfigurationSection localizationSection = configuration.GetSection("Localization")
            ?? throw new ArgumentNullException(nameof(localizationSection));

        string[] supportedCultures = localizationSection.GetSection("SupportedCultures").Get<string[]>()
            ?? throw new ArgumentNullException(nameof(supportedCultures));

        string defaultCulture = localizationSection.GetValue<string>("DefaultCulture")
            ?? throw new ArgumentNullException(nameof(defaultCulture));


        services.AddLocalization();

        services.Configure<RequestLocalizationOptions>(options =>
        {
            options.DefaultRequestCulture = new RequestCulture(defaultCulture);
            options.SupportedCultures = supportedCultures.Select(culture => new CultureInfo(culture)).ToList();
            options.SupportedUICultures = options.SupportedCultures;
        });

        CultureInfo.CurrentCulture = new CultureInfo(defaultCulture);
        CultureInfo.CurrentUICulture = new CultureInfo(defaultCulture);

        return services;
    }

    public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();

        services.AddSwaggerGen(opts =>
        {
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

            opts.IncludeXmlComments(xmlPath, true);
            opts.CustomSchemaIds(type => type.FullName);
            opts.UseAllOfToExtendReferenceSchemas();
            opts.UseAllOfForInheritance();
            opts.UseOneOfForPolymorphism();
            opts.UseInlineDefinitionsForEnums();
            opts.SelectDiscriminatorNameUsing(_ => "$type");
        });

        return services;
    }
}
