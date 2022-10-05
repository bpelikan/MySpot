using MySpot.Application.Commands;
using MySpot.Application.DTO;
using MySpot.Core.Entities;
using MySpot.Core.Repositories;
using MySpot.Core.ValueObjects;

namespace MySpot.Application.Services
{
    public sealed class ReservationsService : IReservationsService
    {
        private readonly IClock _clock;
        private readonly IWeeklyParkingSpotRepository _weeklyParkingSpotRepository;

        public ReservationsService(IClock clock, IWeeklyParkingSpotRepository weeklyParkingSpotRepository)
        {
            _clock = clock;
            _weeklyParkingSpotRepository = weeklyParkingSpotRepository;
        }

        public async Task<ReservationDto> GetAsync(Guid id)
        { 
            var reservation = await GetAllWeeklyAsync();
            return reservation.SingleOrDefault(x => x.Id == id);
        }

        public async Task<IEnumerable<ReservationDto>> GetAllWeeklyAsync()
        {
            var weeklyParkingSpots = await _weeklyParkingSpotRepository.GetAllAsync();
            return weeklyParkingSpots.SelectMany(x => x.Reservations)
                .Select(x => new ReservationDto
                {
                    Id = x.Id,
                    ParkingSpotId = x.ParkingSpotId,
                    EmployeeName = x.EmployeeName,
                    Date = x.Date.Value.Date
                });
        }

        public async Task<Guid?> CreateAsync(CreateReservation command)
        {
            var parkingSpotId = new ParkingSpotId(command.ParkingSpotId);
            var weeklyParkingSpot = await _weeklyParkingSpotRepository.GetAsync(parkingSpotId);
            if(weeklyParkingSpot is null)
                return default;

            var reservation = new Reservation(command.ReservationId, command.ParkingSpotId, command.EmployeeName, 
                command.LicensePlate, new Date(command.Date));
            weeklyParkingSpot.AddReservation(reservation, new Date(_clock.Current()));
            await _weeklyParkingSpotRepository.UdpadeAsync(weeklyParkingSpot);
            
            return reservation.Id;
        }

        public async Task<bool> UpdateAsync(ChangeReservationLicensePlate command)
        {
            var weeklyParkingSpot = await GetWeeklyParkingSpotByReservationAsync(command.ReservationId);
            if (weeklyParkingSpot is null)
                return false;

            var reservationId = new ReservationId(command.ReservationId);
            var existingReservation = weeklyParkingSpot.Reservations.SingleOrDefault(x => x.Id == reservationId);
            if(existingReservation is null)
                return false;

            if(existingReservation.Date.Value.Date <= _clock.Current())
                return false;

            existingReservation.ChangeLicencePlate(command.LicencePlate);
            await _weeklyParkingSpotRepository.UdpadeAsync(weeklyParkingSpot);

            return true;
        }

        public async Task<bool> DeleteAsync(DeleteReservation command)
        {
            var weeklyParkingSpot = await GetWeeklyParkingSpotByReservationAsync(command.ReservationId);
            if (weeklyParkingSpot is null)
                return false;

            var reservationId = new ReservationId(command.ReservationId);
            var existingReservation = weeklyParkingSpot.Reservations.SingleOrDefault(x => x.Id == reservationId);
            if(existingReservation is null)
                return false;

            weeklyParkingSpot.RemoveReservation(command.ReservationId);
            await _weeklyParkingSpotRepository.UdpadeAsync(weeklyParkingSpot);
            //_weeklyParkingSpotRepository.Delete(weeklyParkingSpot); //-> update, do usuniêcia jest rezerwacja, nie miejsce parkingowe

            return true;
        }

        private async Task<WeeklyParkingSpot> GetWeeklyParkingSpotByReservationAsync(ReservationId reservationId)
        {
            var weeklyParkingSpots = await _weeklyParkingSpotRepository.GetAllAsync();
            return weeklyParkingSpots.SingleOrDefault(x => x.Reservations.Any(r => r.Id == reservationId));
        }
    }
}