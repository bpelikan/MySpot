using MySpot.Api.Exceptions;

namespace MySpot.Api.Entities
{
    public class Reservation
    {
        public Guid Id { get; }
        public Guid ParkingSpotId { get; private set; }
        public string EmployeeName { get; private set; }
        public string LicencePlate { get; private set; }
        public DateTime Date { get; private set; }

        public Reservation(Guid id, Guid parkingSpotId, string employeeName, string licencePlate, DateTime date)
        {
            Id = id;
            ParkingSpotId = parkingSpotId;
            EmployeeName = employeeName;
            ChangeLicencePlate(licencePlate);
            Date = date;
        }

        public void ChangeLicencePlate(string licencePlate)
        {
            if(string.IsNullOrWhiteSpace(licencePlate))
                throw new EmptyLicensePlateException();

            LicencePlate = licencePlate;
        }
    }
}