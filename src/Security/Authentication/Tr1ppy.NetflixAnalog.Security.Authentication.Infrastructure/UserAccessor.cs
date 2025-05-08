using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace Tr1ppy.NetflixAnalog.Security.Authentication.Infrastructure;

using Contracts;
using DependencyInjection.Attributes;

[AutoregistredService(DependencyInjection.Enums.ServiceLifetime.Scoped)]
public class UserAccessor(IHttpContextAccessor httpContextAccessor) : IUserAccessor
{
    private readonly ClaimsPrincipal _currentUser = httpContextAccessor.HttpContext?.User
        ?? throw new ArgumentNullException(nameof(httpContextAccessor));

    public Guid GetCurrentUserId()
    {
        Claim? identificatorClaim = _currentUser.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier);
        if (!Guid.TryParse(identificatorClaim?.Value, out Guid userId))
        {
            return Guid.Empty;
        }

        return userId;
    }
}
