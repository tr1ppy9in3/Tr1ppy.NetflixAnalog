namespace Tr1ppy.NetflixAnalog.Security.Authentication.Core;

public abstract class User
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public UserRole[] Roles { get; set; } = Array.Empty<UserRole>();

    public required string Login { get; set; }

    public required string PasswordHash { get; set; }

    public string Email { get; set; } = string.Empty;
}
