using System.Security.Claims;

namespace Tr1ppy.NetflixAnalog.Security.Authentication.Core;

public class User
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public UserRole[] Roles { get; set; } = Array.Empty<UserRole>();

    public required string Login { get; set; }

    public required string PasswordHash { get; set; }

    public string Email { get; set; } = string.Empty;

    public bool IsBlocked { get; set; } = false;

    public Claim[] GetClaims()
    {
        List<Claim> claims =
        [
            new Claim(ClaimTypes.NameIdentifier, Id.ToString()),
            .. Roles.Select(role => new Claim(ClaimTypes.Role, role.ToString())),
        ];

        return [.. claims];
    }
}
