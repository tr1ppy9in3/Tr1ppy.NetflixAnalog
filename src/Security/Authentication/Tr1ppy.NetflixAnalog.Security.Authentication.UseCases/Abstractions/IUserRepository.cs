using Tr1ppy.NetflixAnalog.Security.Authentication.Core;

namespace Tr1ppy.NetflixAnalog.Security.Authentication.UseCases.Abstractions;

public interface IUserRepository
{
    public Task<User?> GetByLoginAsync(string login);

    public Task<bool> ExistsByLogin(string login);

    public Task Create(User user);
}
