using System.Reflection;
using FluentValidation;
using MediatR;
using MediatR.Registration;
using Microsoft.Extensions.DependencyInjection;
using ProgrammerGrammar.Accounts.Core.Behaviors;

namespace ProgrammerGrammar.Accounts.Core
{
    public static class Setup
    {
        public static IServiceCollection AddCore(this IServiceCollection services)
        {
            ServiceRegistrar.AddMediatRClasses(services, new[] { Assembly.GetExecutingAssembly() });

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestLoggingBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPerformanceBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));

            AssemblyScanner.FindValidatorsInAssembly(Assembly.GetExecutingAssembly()).ForEach(item => services.AddTransient(item.InterfaceType, item.ValidatorType));

            return services;
        }
    }
}