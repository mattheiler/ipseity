using System.Reflection;
using FluentValidation;
using MediatR.Registration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProgrammerGrammar.Example.Core.Abstractions;
using ProgrammerGrammar.Example.Infrastructure.Data;

namespace ProgrammerGrammar.Example.Infrastructure
{
    public static class Setup
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            ServiceRegistrar.AddMediatRClasses(services, new[] { Assembly.GetExecutingAssembly() });

            AssemblyScanner.FindValidatorsInAssembly(Assembly.GetExecutingAssembly()).ForEach(item => services.AddTransient(item.InterfaceType, item.ValidatorType));

            services
                .AddDbContext<ExampleDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("Example")))
                .AddTransient<IExampleDbContext, ExampleDbContext>(provider => provider.GetService<ExampleDbContext>());

            return services;
        }
    }
}