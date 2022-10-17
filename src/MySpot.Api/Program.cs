using MySpot.Infrastructure;
using MySpot.Application;
using MySpot.Core;
using Serilog;
using MySpot.Infrastructure.Logging;

var builder = WebApplication.CreateBuilder(args);
builder.Services
    .AddCore()
    .AddApplication()
    .AddInfrastructure(builder.Configuration)
    .AddControllers();

builder.UseSerilog();

var app = builder.Build();

app.UseInfrastructure();
app.Run();


//app.Use(async (ctx, next) =>
//{
//    Console.WriteLine("Step 1 start");
//    await next(ctx);
//    Console.WriteLine("Step 1 finish");
//});

//app.Use(async (ctx, next) =>
//{
//    Console.WriteLine("Step 2 start");
//    await next(ctx);
//    Console.WriteLine("Step 2 finish");
//});





