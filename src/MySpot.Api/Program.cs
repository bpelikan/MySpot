using MySpot.Infrastructure;
using MySpot.Application;
using MySpot.Core;

var builder = WebApplication.CreateBuilder(args);
builder.Services
    .AddCore()
    .AddApplication()
    .AddInfrastructure(builder.Configuration)
    .AddControllers();

var app = builder.Build();

app.MapControllers();
app.Run();
