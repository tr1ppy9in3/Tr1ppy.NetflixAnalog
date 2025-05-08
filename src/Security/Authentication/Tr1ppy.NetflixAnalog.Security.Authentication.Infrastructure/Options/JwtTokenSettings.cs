namespace Tr1ppy.NetflixAnalog.Security.Authentication.Infrastructure.Options;

public class JwtTokenSettings
{
    public required string ValidIssusier { get; set; }

    public required string TokenSecretKey { get; set; }

    public required int AccessTokenLifetimeInSeconds {  get; set; }

    public required int RefreshTokenLifetimeInSeconds { get; set; }  
}
