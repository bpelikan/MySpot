using MySpot.Application.Abstractions;

namespace MySpot.Application.Commands
{
    public record ReserveParkingSpotForVehicle(Guid ParkingSpotId, Guid ReservationId, int capacity,
        DateTime Date, Guid UserId, string LicensePlate) : ICommand;

}