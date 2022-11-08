﻿using Microsoft.EntityFrameworkCore;
using MySpot.Infrastructure.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySpot.Tests.Integration
{
    internal class TestDatabase : IDisposable
    {
        public MySpotDbContext Context { get; }

        public TestDatabase()
        {
            var options = new OptionsProvider().Get<PostgresOptions>("postgres");
            Context = new MySpotDbContext(new DbContextOptionsBuilder<MySpotDbContext>()
                .UseNpgsql(options.ConnectionString).Options);
        }

        public void Dispose()
        {
            Context.Database.EnsureDeleted();
            Context.Dispose();
        }
    }
}
