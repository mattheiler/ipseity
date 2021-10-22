using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace ProgrammerGrammar.WispyWaterfall.Infrastructure.Data
{
    public class WispyWaterfallDbContextDesignTimeFactory : IDesignTimeDbContextFactory<WispyWaterfallDbContext>
    {
        public WispyWaterfallDbContext CreateDbContext(string[] args)
        {
            var configuration =
                new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .AddEnvironmentVariables()
                    .AddUserSecrets<WispyWaterfallDbContextDesignTimeFactory>(true)
                    .Build();
            var options =
                new DbContextOptionsBuilder<WispyWaterfallDbContext>()
                    .UseSqlServer(configuration.GetConnectionString("WispyWaterfall"))
                    .Options;
            return new WispyWaterfallDbContext(options);
        }
    }
}