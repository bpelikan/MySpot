using MySpot.Api.Entities;

namespace MySpot.Api.Services
{
    public sealed class ReservationsService
    {
        private static WeeklyParkingSpot[] WeeklyParkingSpots = 
        {
            new (Guid.NewGuid(), DateTime.UtcNow, DateTime.UtcNow.AddDays(7), "P1"),
            new (Guid.NewGuid(), DateTime.UtcNow, DateTime.UtcNow.AddDays(7), "P2"),
            new (Guid.NewGuid(), DateTime.UtcNow, DateTime.UtcNow.AddDays(7), "P3"),
            new (Guid.NewGuid(), DateTime.UtcNow, DateTime.UtcNow.AddDays(7), "P4"),
            new (Guid.NewGuid(), DateTime.UtcNow, DateTime.UtcNow.AddDays(7), "P5"),
        };

        public IEnumerable<Reservation> GetAllWeekly() 
            => WeeklyParkingSpots.SelectMany(x => x.Reservations);

        public Reservation Get(Guid id)
            => GetAllWeekly().SingleOrDefault(x => x.Id == id);

        public Guid? Create(Reservation reservation)
        {
            var weeklyParkingSpot = WeeklyParkingSpots.SingleOrDefault(x => x.Id == reservation.ParkingSpotId);
            if(weeklyParkingSpot is null)
                return default;

            reservation.Id = Guid.NewGuid();
            weeklyParkingSpot.AddReservation(reservation);
            return reservation.Id;
        }

        public bool Update(Reservation reservation)
        {
            var existingReservation = _reservations.SingleOrDefault(x => x.Id == reservation.Id);
            if(existingReservation is null)
                return false;

            if(existingReservation.Date <= DateTime.UtcNow.Date)
                return false;

            existingReservation.LicencePlate = reservation.LicencePlate;
            return true;
        }

        public bool Delete(int id)
        {
            var existingReservation = _reservations.SingleOrDefault(x => x.Id == id);
            if(existingReservation is null)
                return false;

            return _reservations.Remove(existingReservation);
        }

    }
}