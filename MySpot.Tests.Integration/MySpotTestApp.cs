using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySpot.Tests.Integration
{
    internal class MySpotTestApp : WebApplicationFactory<Program>
    {
        public HttpClient Client { get; }

        public MySpotTestApp(Action<IServiceCollection> services = null)
        {
            Client = WithWebHostBuilder(builder =>
            {
                if (services is not null)
                {
                    builder.ConfigureServices(services);
                }

                builder.UseEnvironment("test");
            }).CreateClient();
        }
    }
}
