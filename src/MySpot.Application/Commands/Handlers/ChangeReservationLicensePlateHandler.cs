using MySpot.Application.Abstractions;
using MySpot.Application.Exceptions;
using MySpot.Core.Abstractions;
using MySpot.Core.Entities;
using MySpot.Core.Exceptions;
using MySpot.Core.Repositories;
using MySpot.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySpot.Application.Commands.Handlers
{
    internal sealed class ChangeReservationLicensePlateHandler : ICommandHandler<ChangeReservationLicensePlate>
    {
        private readonly IClock _clock;
        private readonly IWeeklyParkingSpotRepository _weeklyParkingSpotRepository;

        public ChangeReservationLicensePlateHandler(IWeeklyParkingSpotRepository weeklyParkingSpotRepository, IClock clock)
        {
            _weeklyParkingSpotRepository = weeklyParkingSpotRepository;
            _clock = clock;
        }

        public async Task HandleAsync(ChangeReservationLicensePlate command)
        {
            var weeklyParkingSpot = await GetWeeklyParkingSpotByReservationAsync(command.ReservationId);
            if (weeklyParkingSpot is null)
                throw new WeeklyParkingSpotNotFoundException();

            var reservationId = new ReservationId(command.ReservationId);
            var reservation = weeklyParkingSpot.Reservations
                                                .OfType<VehicleReservation>()
                                                .SingleOrDefault(x => x.Id == reservationId);
            if (reservation is null)
                throw new ReservationNotFoundException(command.ReservationId);

            if (reservation.Date.Value.Date <= _clock.Current())
                throw new InvalidReservationDateException(reservation.Date.Value.Date); 
                    //TODO: czy exception powinien być w Application, czy może korzystać z Core exception?

            reservation.ChangeLicencePlate(command.LicencePlate);
            await _weeklyParkingSpotRepository.UdpadeAsync(weeklyParkingSpot);
        }

        private async Task<WeeklyParkingSpot> GetWeeklyParkingSpotByReservationAsync(ReservationId id)
            => (await _weeklyParkingSpotRepository.GetAllAsync())
                .SingleOrDefault(x => x.Reservations.Any(r => r.Id == id));
    }
}
