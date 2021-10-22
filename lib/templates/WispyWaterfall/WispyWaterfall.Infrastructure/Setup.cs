using System.Reflection;
using FluentValidation;
using MediatR.Registration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProgrammerGrammar.WispyWaterfall.Core.Abstractions;
using ProgrammerGrammar.WispyWaterfall.Infrastructure.Data;

namespace ProgrammerGrammar.WispyWaterfall.Infrastructure
{
    public static class Setup
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            ServiceRegistrar.AddMediatRClasses(services, new[] { Assembly.GetExecutingAssembly() });

            AssemblyScanner.FindValidatorsInAssembly(Assembly.GetExecutingAssembly()).ForEach(item => services.AddTransient(item.InterfaceType, item.ValidatorType));

            services
                .AddDbContext<ExampleDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("WispyWaterfall")))
                .AddTransient<IExampleDbContext, ExampleDbContext>(provider => provider.GetService<ExampleDbContext>());

            return services;
        }
    }
}