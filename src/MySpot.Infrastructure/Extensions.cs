﻿using Microsoft.Extensions.DependencyInjection;
using MySpot.Application.Services;
using MySpot.Core.Repositories;
using MySpot.Infrastructure.DAL;
using MySpot.Infrastructure.DAL.Repositories;
using MySpot.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

//[assembly: InternalsVisibleTo("MySpot.Tests.Unit")]
namespace MySpot.Infrastructure
{
    public static class Extensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services
                .AddPostgres()
                .AddSingleton<IClock, Clock>();
                //.AddSingleton<IWeeklyParkingSpotRepository, InMemoryWeeklyParkingSpotRepository>();

            return services;
        }
    }
}
