using MySpot.Application.Abstractions;
using MySpot.Application.Exceptions;
using MySpot.Core.Entities;
using MySpot.Core.Repositories;
using MySpot.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySpot.Application.Commands.Handlers
{
    internal sealed class DeleteReservationHandler : ICommandHandler<DeleteReservation>
    {
        private readonly IWeeklyParkingSpotRepository _weeklyParkingSpotRepository;

        public DeleteReservationHandler(IWeeklyParkingSpotRepository weeklyParkingSpotRepository)
        {
            _weeklyParkingSpotRepository = weeklyParkingSpotRepository;
        }

        public async Task HandleAsync(DeleteReservation command)
        {
            var weeklyParkingSpot = await GetWeeklyParkingSpotByReservationAsync(command.ReservationId);
            if (weeklyParkingSpot is null)
                throw new WeeklyParkingSpotNotFoundException();

            //var reservationId = new ReservationId(command.ReservationId);
            //var existingReservation = weeklyParkingSpot.Reservations.SingleOrDefault(x => x.Id == reservationId);
            //if (existingReservation is null)
            //    return false;

            weeklyParkingSpot.RemoveReservation(command.ReservationId); 
            await _weeklyParkingSpotRepository.UdpadeAsync(weeklyParkingSpot);
        }

        private async Task<WeeklyParkingSpot> GetWeeklyParkingSpotByReservationAsync(ReservationId id)
            => (await _weeklyParkingSpotRepository.GetAllAsync())
                .SingleOrDefault(x => x.Reservations.Any(r => r.Id == id));
    }
}
