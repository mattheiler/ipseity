﻿using System;
using System.Threading;
using System.Threading.Tasks;
using IdentityServer4.EntityFramework.Entities;
using IdentityServer4.EntityFramework.Extensions;
using IdentityServer4.EntityFramework.Interfaces;
using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.DataProtection.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Options;
using ProgrammerGrammar.Accounts.Core.Abstractions;

namespace ProgrammerGrammar.Accounts.Infrastructure.Data
{
    public class AccountsDbContext : IdentityDbContext, IDataProtectionKeyContext, IPersistedGrantDbContext, IAccountsDbContext
    {
        private readonly IOptions<OperationalStoreOptions> _operationalStoreOptions;

        public AccountsDbContext(DbContextOptions<AccountsDbContext> options, IOptions<OperationalStoreOptions> operationalStoreOptions)
            : base(options)
        {
            _operationalStoreOptions = operationalStoreOptions;
        }

        public DbSet<DataProtectionKey> DataProtectionKeys { get; set; }

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

        public DbSet<PersistedGrant> PersistedGrants { get; set; }

        public DbSet<DeviceFlowCodes> DeviceFlowCodes { get; set; }

        Task<int> IPersistedGrantDbContext.SaveChangesAsync()
        {
            return SaveChangesAsync();
        }

        protected async Task LockAsync(string table, CancellationToken cancellationToken = default)
        {
            await Database.ExecuteSqlRawAsync($"SELECT * FROM {table} WITH (TABLOCKX)", cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ConfigurePersistedGrantContext(_operationalStoreOptions.Value);
        }
    }
}