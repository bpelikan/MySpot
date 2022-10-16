using Microsoft.Extensions.DependencyInjection;
using MySpot.Application.Abstractions;

namespace MySpot.Application
{
    public static class Extensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            //var applicationAssembly = typeof(Extensions).Assembly;
            var applicationAssembly = typeof(ICommandHandler<>).Assembly;

            services.Scan(s => s.FromAssemblies(applicationAssembly)
                .AddClasses(x => x.AssignableTo(typeof(ICommandHandler<>)))
                .AsImplementedInterfaces()
                .WithScopedLifetime()
            );
            return services;
        }
    }
}
