using MySpot.Api.Exceptions;
using MySpot.Api.ValueObjects;

namespace MySpot.Api.Entities
{
    public class WeeklyParkingSpot
    {
        private readonly HashSet<Reservation> _reservations = new();

        public ParkingSpotId Id { get; }
        public Week Week { get; }
        public string Name { get; }

        public IEnumerable<Reservation> Reservations => _reservations; //dojść do rezerwacji i sprawdzić czy można przypisać inną wartośc np rejestracji

        public WeeklyParkingSpot(ParkingSpotId id, Week week, string name)
        {
            Id = id;
            Week = week;
            Name = name;
        }

        public void AddReservation(Reservation reservation, Date now)
        {
            var isInvalidDate = reservation.Date < Week.From || 
                                reservation.Date > Week.To || 
                                reservation.Date < now;
            if(isInvalidDate)
                throw new InvalidReservationDateException(reservation.Date.Value.Date);
                
            var alreadyReserved = _reservations.Any(x => x.Date == reservation.Date);
            if(alreadyReserved)
                throw new ParkingSpotAlreadyReservedException(Name, reservation.Date.Value.Date);
            
            _reservations.Add(reservation);
        }

        public void RemoveReservation(ReservationId reservationId)
            => _reservations.RemoveWhere(x => x.Id == reservationId);

    }
}