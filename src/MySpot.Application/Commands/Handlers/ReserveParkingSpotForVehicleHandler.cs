using MySpot.Application.Abstractions;
using MySpot.Application.Exceptions;
using MySpot.Core.Abstractions;
using MySpot.Core.DomainServices;
using MySpot.Core.Entities;
using MySpot.Core.Repositories;
using MySpot.Core.ValueObjects;

namespace MySpot.Application.Commands.Handlers
{
    internal sealed class ReserveParkingSpotForVehicleHandler : ICommandHandler<ReserveParkingSpotForVehicle>
    {
        private readonly IClock _clock;
        private readonly IWeeklyParkingSpotRepository _repository;
        private readonly IParkingReservationService _reservationService;
        private readonly IUserRepository _userRepository;

        public ReserveParkingSpotForVehicleHandler(IClock clock, IWeeklyParkingSpotRepository weeklyParkingSpotRepository,
            IParkingReservationService parkingReservationService, IUserRepository userRepository)
        {
            _clock = clock;
            _repository = weeklyParkingSpotRepository;
            _reservationService = parkingReservationService;
            _userRepository = userRepository;
        }

        public async Task HandleAsync(ReserveParkingSpotForVehicle command)
        {
            var (spotId, reservationId, capacity, date, userId, licencePlate) = command;
            var week = new Week(_clock.Current());
            var parkingSpotId = new ParkingSpotId(command.ParkingSpotId);
            var weeklyParkingSpots = (await _repository.GetByWeekAsync(week)).ToList();
            var parkingSpotToReserve = weeklyParkingSpots.SingleOrDefault(x => x.Id == parkingSpotId);
            
            if (parkingSpotToReserve is null)
                throw new WeeklyParkingSpotNotFoundException(parkingSpotId);

            var user = await _userRepository.GetByIdAsync(userId);
            if (user is null)
            {
                throw new UserNotFoundException(userId);
            }

            var reservation = new VehicleReservation(reservationId, spotId, user.Id, new EmployeeName(user.FullName),
                licencePlate, capacity, new Date(date));

            _reservationService.ReserveSpotForVehicle(weeklyParkingSpots, JobTitle.Employee, parkingSpotToReserve, reservation);
            await _repository.UdpadeAsync(parkingSpotToReserve);
        }
    }
}
