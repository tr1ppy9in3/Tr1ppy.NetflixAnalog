using Microsoft.EntityFrameworkCore;

namespace Tr1ppy.NetflixAnalog.Security.Authentication.DataAccess;

using Authentication.Core;
using Configurations;

public class AuthenticationDataContext : DbContext
{
    public DbSet<User> Users { get; set; }

    public AuthenticationDataContext(DbContextOptions<AuthenticationDataContext> option) : base(option)
    {
        Database.Migrate();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserConfiguration).Assembly);
    }
}
