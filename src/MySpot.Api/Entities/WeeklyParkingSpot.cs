using MySpot.Api.Exceptions;

namespace MySpot.Api.Entities
{
    public class WeeklyParkingSpot
    {
        private readonly HashSet<Reservation> _reservations = new();

        public Guid Id { get; }
        public DateTime From { get; }
        public DateTime To { get; }
        public string Name { get; }

        public IEnumerable<Reservation> Reservations => _reservations; //dojść do rezerwacji i sprawdzić czy można przypisać inną wartośc np rejestracji

        public WeeklyParkingSpot(Guid id, DateTime from, DateTime to, string name)
        {
            Id = id;
            From = from;
            To = to;
            Name = name;
        }

        public void AddReservation(Reservation reservation, DateTime now)
        {
            var isInvalidDate = reservation.Date.Date < From || 
                                reservation.Date.Date > To || 
                                reservation.Date.Date < now.Date;
            if(isInvalidDate)
                throw new InvalidReservationDateException(reservation.Date);
                
            var alreadyReserved = _reservations.Any(x => x.Date.Date == reservation.Date.Date);
            if(alreadyReserved)
                throw new ParkingSpotAlreadyReservedException(Name, reservation.Date);
            
            _reservations.Add(reservation);
        }

        public void RemoveReservation(Guid id)
            => _reservations.RemoveWhere(x => x.Id == id);

    }
}