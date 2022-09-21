using System.Collections;
using MySpot.Core.Entities;
using MySpot.Api.Repositories;
using MySpot.Api.Services;
using MySpot.Core.ValueObjects;
using MySpot.Core.Repositories;

var builder = WebApplication.CreateBuilder(args);
builder.Services
    .AddSingleton<IClock,  Clock>()
    .AddSingleton<IWeeklyParkingSpotRepository, InMemoryWeeklyParkingSpotRepository>()
    .AddSingleton<IReservationsService, ReservationsService>()
    .AddControllers();

var app = builder.Build();

app.MapControllers();
app.Run();
