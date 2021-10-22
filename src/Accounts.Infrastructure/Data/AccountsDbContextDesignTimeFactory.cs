using System.IO;
using IdentityServer4.EntityFramework.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace ProgrammerGrammar.Accounts.Infrastructure.Data
{
    public class AccountsDbContextDesignTimeFactory : IDesignTimeDbContextFactory<AccountsDbContext>
    {
        public AccountsDbContext CreateDbContext(string[] args)
        {
            var configuration =
                new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .AddEnvironmentVariables()
                    .AddUserSecrets<AccountsDbContextDesignTimeFactory>(true)
                    .Build();
            var options =
                new DbContextOptionsBuilder<AccountsDbContext>()
                    .UseSqlServer(configuration.GetConnectionString("Accounts"))
                    .Options;
            return new AccountsDbContext(options, Options.Create(new OperationalStoreOptions()));
        }
    }
}