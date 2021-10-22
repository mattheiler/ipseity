using System.Reflection;
using FluentValidation;
using MediatR.Registration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProgrammerGrammar.Accounts.Core.Abstractions;
using ProgrammerGrammar.Accounts.Infrastructure.Data;

namespace ProgrammerGrammar.Accounts.Infrastructure
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