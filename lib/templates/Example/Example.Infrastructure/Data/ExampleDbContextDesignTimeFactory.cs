using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace ProgrammerGrammar.Example.Infrastructure.Data
{
    public class ExampleDbContextDesignTimeFactory : IDesignTimeDbContextFactory<ExampleDbContext>
    {
        public ExampleDbContext CreateDbContext(string[] args)
        {
            var configuration =
                new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .AddEnvironmentVariables()
                    .AddUserSecrets<ExampleDbContextDesignTimeFactory>(true)
                    .Build();
            var options =
                new DbContextOptionsBuilder<ExampleDbContext>()
                    .UseSqlServer(configuration.GetConnectionString("Example"))
                    .Options;
            return new ExampleDbContext(options);
        }
    }
}