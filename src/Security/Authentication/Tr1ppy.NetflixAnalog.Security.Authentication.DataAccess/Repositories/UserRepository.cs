using Microsoft.EntityFrameworkCore;

namespace Tr1ppy.NetflixAnalog.Security.Authentication.DataAccess.Repositories;

using Core;
using UseCases.Abstractions;
using DependencyInjection.Attributes;

[AutoregistredService(DependencyInjection.Enums.ServiceLifetime.Scoped)]
public class UserRepository(AuthenticationDataContext authenticationDataContext) : IUserRepository
{
    private readonly AuthenticationDataContext _authenticationDataContext = authenticationDataContext
        ?? throw new ArgumentNullException(nameof(authenticationDataContext));

    private readonly DbSet<User> _users 
        = authenticationDataContext.Users;

    public Task Create(User user)
    {
        _users.Add(user);
        return _authenticationDataContext.SaveChangesAsync();
    }

    public Task<bool> ExistsByLogin(string login)
    {
        return 
            _users.AnyAsync(user => string.Equals(user.Login, login));
    }

    public Task<User?> GetByLoginAsync(string login)
    {
        return 
            _users.FirstOrDefaultAsync(user => string.Equals(user.Login, login));
    }
}
