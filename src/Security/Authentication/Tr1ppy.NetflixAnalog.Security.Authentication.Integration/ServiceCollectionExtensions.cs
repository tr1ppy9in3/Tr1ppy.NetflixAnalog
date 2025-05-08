using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using System.Text;

namespace Tr1ppy.NetflixAnalog.Security.Authentication.Integration;

using Infrastructure.Options;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDefaultAunthentication
    (
        this IServiceCollection services, 
        IConfiguration configuration
    )
    {
        var jwtTokenSettings = Configure(services, configuration);

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtTokenSettings.TokenSecretKey));
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.RequireHttpsMetadata = false;
            options.SaveToken = true;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = jwtTokenSettings.ValidIssusier,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = key,
            };
        });


        return services;
    }

    private static JwtTokenSettings Configure
    (
        IServiceCollection services, 
        IConfiguration configuration
    )
    {
        IConfigurationSection securitySection = configuration.GetSection("Security");
        IConfigurationSection jwtTokenSection = securitySection.GetSection("JwtTokenSettings");
        services.Configure<JwtTokenSettings>(jwtTokenSection);

        JwtTokenSettings? jwtTokenSettings = jwtTokenSection.Get<JwtTokenSettings>()
            ?? throw new ArgumentNullException(nameof(jwtTokenSection));

        return jwtTokenSettings;
    }
}
