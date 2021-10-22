using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using ProgrammerGrammar.WispyWaterfall.Core.Abstractions;

namespace ProgrammerGrammar.WispyWaterfall.Infrastructure.Data
{
    public class WispyWaterfallDbContext : DbContext, IWispyWaterfallDbContext
    {
        public WispyWaterfallDbContext(DbContextOptions<WispyWaterfallDbContext> options)
            : base(options)
        {
        }

        public IDbContextTransaction BeginTransaction()
        {
            return Database.BeginTransaction();
        }

        public Task LockAsync<T>(CancellationToken cancellationToken = default)
            where T : class
        {
            return LockAsync(typeof(T), cancellationToken);
        }

        public Task LockAsync(Type type, CancellationToken cancellationToken = default)
        {
            return LockAsync(Model.FindEntityType(type).GetTableName(), cancellationToken);
        }

        protected async Task LockAsync(string table, CancellationToken cancellationToken = default)
        {
            await Database.ExecuteSqlRawAsync($"SELECT * FROM {table} WITH (TABLOCKX)", cancellationToken);
        }
    }
}