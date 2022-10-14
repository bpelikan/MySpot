using MySpot.Application.Commands;
using MySpot.Application.DTO;
using MySpot.Core.Abstractions;
using MySpot.Core.DomainServices;
using MySpot.Core.Entities;
using MySpot.Core.Repositories;
using MySpot.Core.ValueObjects;

namespace MySpot.Application.Services
{
    public sealed class ReservationsService : IReservationsService
    {
        private readonly IClock _clock;
        private readonly IWeeklyParkingSpotRepository _weeklyParkingSpotRepository;
        private readonly IParkingReservationService _parkingReservationService;

        public ReservationsService(IClock clock, IWeeklyParkingSpotRepository weeklyParkingSpotRepository, 
            IParkingReservationService parkingReservationService)
        {
            _clock = clock;
            _weeklyParkingSpotRepository = weeklyParkingSpotRepository;
            _parkingReservationService = parkingReservationService;
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
                    EmployeeName = x is VehicleReservation vr ? vr.EmployeeName : String.Empty,
                    Date = x.Date.Value.Date
                });
        }

        public async Task<Guid?> ReserveForVehicleAsync(ReserveParkingSpotForVehicle command)
        {
            var parkingSpotId = new ParkingSpotId(command.ParkingSpotId);
            var week = new Week(_clock.Current());

            var weeklyParkingSpots = (await _weeklyParkingSpotRepository.GetByWeekAsync(week)).ToList();
            var parkingSpotToReserve = weeklyParkingSpots.SingleOrDefault(x => x.Id == parkingSpotId);
            if (parkingSpotToReserve is null)
                return default;

            var reservation = new VehicleReservation(command.ReservationId, command.ParkingSpotId, command.EmployeeName, 
                command.LicensePlate, command.capacity, new Date(command.Date));

            _parkingReservationService.ReserveSpotForVehicle(weeklyParkingSpots, JobTitle.Employee, parkingSpotToReserve, reservation);
            await _weeklyParkingSpotRepository.UdpadeAsync(parkingSpotToReserve);
            
            return reservation.Id;
        }


        public async Task ReserveForCleaningAsync(ReserveParkingSpotForCleaning command)
        {
            //TODO: date in current week checking
            var week = new Week(command.date);
            var weeklyParkingSpots = (await _weeklyParkingSpotRepository.GetByWeekAsync(week)).ToList();

            _parkingReservationService.ReserveParkingForCleaning(weeklyParkingSpots, new Date(command.date));

            //var tasks = weeklyParkingSpots.Select(x => _weeklyParkingSpotRepository.UdpadeAsync(x));
            //await Task.WhenAll(tasks);

            foreach (var parkingSpot in weeklyParkingSpots)
            {
                await _weeklyParkingSpotRepository.UdpadeAsync(parkingSpot);
            }

        }


        public async Task<bool> ChangeReservationLicensePlateAsync(ChangeReservationLicensePlate command)
        {
            var weeklyParkingSpot = await GetWeeklyParkingSpotByReservationAsync(command.ReservationId);
            if (weeklyParkingSpot is null)
                return false;

            var reservationId = new ReservationId(command.ReservationId);
            var existingReservation = weeklyParkingSpot.Reservations
                .OfType<VehicleReservation>()
                .SingleOrDefault(x => x.Id == reservationId);
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