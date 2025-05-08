using System.Text;
using System.IdentityModel.Tokens.Jwt;

using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Tr1ppy.NetflixAnalog.Security.Authentication.Infrastructure;

using Options;

using Core;
using UseCases.Abstractions;
using DependencyInjection.Attributes;

[AutoregistredService(DependencyInjection.Enums.ServiceLifetime.Scoped)]
public class TokenService(IOptions<JwtTokenSettings> options) : ITokenService
{
    private readonly JwtTokenSettings _settings = options.Value
        ?? throw new ArgumentNullException(nameof(options));

    public string GenerateAccessToken(User user)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.TokenSecretKey));
        var creditiants = new SigningCredentials(key, SecurityAlgorithms.Aes128KeyWrap);

        var token = new JwtSecurityToken
        (
            issuer: _settings.ValidIssusier,
            audience: "User",
            claims: user.GetClaims(),
            expires: DateTime.UtcNow.AddSeconds(_settings.AccessTokenLifetimeInSeconds),
            signingCredentials: creditiants
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public string GenerateRefreshToken(User user)
    {
        throw new NotImplementedException();
    }
}