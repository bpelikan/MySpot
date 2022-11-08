using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MySpot.Application.Abstractions;
using MySpot.Infrastructure.Logging.Decorators;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySpot.Infrastructure.Logging
{
    public static class Extensions
    {
        internal static IServiceCollection AddCustomLogging(this IServiceCollection services)
        {
            services.TryDecorate(typeof(ICommandHandler<>), typeof(LoggingCommandHandlerDecorator<>));
            return services;
        }

        public static WebApplicationBuilder UseSerilog(this WebApplicationBuilder builder)
        {
            builder.Host.UseSerilog((context, configuration) =>
            {
                configuration
                .WriteTo.Console()
                .WriteTo.File("logs/logs.txt")
                .WriteTo.Seq("http://localhost:5341");
            });

            return builder;
        }

    }
}
