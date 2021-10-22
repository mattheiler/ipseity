using System.Reflection;
using Example.Core.Behaviors;
using FluentValidation;
using MediatR;
using MediatR.Registration;
using Microsoft.Extensions.DependencyInjection;

namespace Example.Core
{
    public static class Setup
    {
        public static IServiceCollection AddCore(this IServiceCollection services)
        {
            ServiceRegistrar.AddMediatRClasses(services, new[] { Assembly.GetExecutingAssembly() });

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestLoggingBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPerformanceBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));

            AssemblyScanner.FindValidatorsInAssembly(Assembly.GetExecutingAssembly()).ForEach(item => services.AddScoped(item.InterfaceType, item.ValidatorType));

            return services;
        }
    }
}