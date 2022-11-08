﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MySpot.Application.Abstractions;
using MySpot.Application.Commands;
using MySpot.Core.Repositories;
using MySpot.Infrastructure.DAL.Decorators;
using MySpot.Infrastructure.DAL.Repositories;
using MySpot.Infrastructure.Logging.Decorators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace MySpot.Infrastructure.DAL
{
    internal static class Extensions
    {
        private const string OptionsSectionName = "postgres";

        public static IServiceCollection AddPostgres(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<PostgresOptions>(configuration.GetSection(OptionsSectionName));
            var postgresOptions = configuration.GetOption<PostgresOptions>(OptionsSectionName);

            services.AddDbContext<MySpotDbContext>(x => x.UseNpgsql(postgresOptions.ConnectionString));
            services.AddScoped<IWeeklyParkingSpotRepository, PostgresWeeklyParkingSpotRepository>();
            services.AddScoped<IUserRepository, PostgresUserRepository>();
            services.AddScoped<IUnitOfWork, PostgressUnitOfWork>();
            services.TryDecorate(typeof(ICommandHandler<>), typeof(UnitOfWorkCommandHandlerDecorator<>));
            services.AddHostedService<DatabaseInitializer>();
            
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

            return services;
        }

          
    }
}
