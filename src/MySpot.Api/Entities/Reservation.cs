using MySpot.Api.Exceptions;
using MySpot.Api.ValueObjects;

namespace MySpot.Api.Entities
{
    public class Reservation
    {
        public ReservationId Id { get; }
        public ParkingSpotId ParkingSpotId { get; private set; }
        public EmployeeName EmployeeName { get; private set; }
        public LicensePlate LicensePlate { get; private set; }
        public Date Date { get; private set; }

        public Reservation(ReservationId id, ParkingSpotId parkingSpotId, EmployeeName employeeName, 
            LicensePlate licencePlate, Date date)
        {
            Id = id;
            ParkingSpotId = parkingSpotId;
            EmployeeName = employeeName;
            ChangeLicencePlate(licencePlate);
            Date = date;
        }

        public void ChangeLicencePlate(LicensePlate licensePlate)
            => LicensePlate = licensePlate;
    }
}