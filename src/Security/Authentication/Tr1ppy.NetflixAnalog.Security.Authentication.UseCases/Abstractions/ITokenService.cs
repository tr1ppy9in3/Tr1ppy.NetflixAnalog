using Tr1ppy.NetflixAnalog.Security.Authentication.Core;

namespace Tr1ppy.NetflixAnalog.Security.Authentication.UseCases.Abstractions;

public interface ITokenService
{
    public string GenerateAccessToken(User user);

    public string GenerateRefreshToken(User user);
}
