using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

using Tr1ppy.EntityFramework;

namespace Tr1ppy.NetflixAnalog.Security.Authentication.DataAccess;

internal class AuthenticationDataContextFactoryForMigration : IDesignTimeDbContextFactory<AuthenticationDataContext>
{
    public AuthenticationDataContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AuthenticationDataContext>();
        var dbSettings = MigrationHelper.FindSettings();

        optionsBuilder.ApplyProvider(dbSettings.Item1, dbSettings.Item2);
        return new AuthenticationDataContext(optionsBuilder.Options);
    }
}
