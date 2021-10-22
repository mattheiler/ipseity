using System.Reflection;
using Accounts.Core.Abstractions;
using Accounts.Infrastructure.Data;
using FluentValidation;
using MediatR.Registration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Accounts.Infrastructure
{
    public static class Setup
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            ServiceRegistrar.AddMediatRClasses(services, new[] { Assembly.GetExecutingAssembly() });

            AssemblyScanner.FindValidatorsInAssembly(Assembly.GetExecutingAssembly()).ForEach(item => services.AddTransient(item.InterfaceType, item.ValidatorType));

            services
                .AddDbContext<AccountsDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("Accounts")))
                .AddTransient<IAccountsDbContext, AccountsDbContext>(provider => provider.GetService<AccountsDbContext>());

            return services;
        }
    }
}