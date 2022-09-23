using System.Collections;
using MySpot.Api.Repositories;
using MySpot.Core.Repositories;
using MySpot.Application.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services
    .AddSingleton<IClock, Clock>()
    .AddSingleton<IWeeklyParkingSpotRepository, InMemoryWeeklyParkingSpotRepository>()
    .AddSingleton<IReservationsService, ReservationsService>()
    .AddControllers();

var app = builder.Build();

app.MapControllers();
app.Run();
