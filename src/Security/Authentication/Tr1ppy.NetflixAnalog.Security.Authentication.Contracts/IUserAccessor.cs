namespace Tr1ppy.NetflixAnalog.Security.Authentication.Contracts;

public interface IUserAccessor
{
    public Guid GetCurrentUserId();
}
