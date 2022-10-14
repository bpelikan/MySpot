namespace MySpot.Application.Commands
{
    public record ReserveParkingSpotForVehicle(Guid ParkingSpotId, Guid ReservationId, int capacity,
        DateTime Date, string EmployeeName, string LicensePlate);

}