using System.Reflection;
using Example.Core.Abstractions;
using Example.Infrastructure.Data;
using FluentValidation;
using MediatR.Registration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Example.Infrastructure
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