using MySpot.Application.Abstractions;
using MySpot.Core.Abstractions;
using MySpot.Core.DomainServices;
using MySpot.Core.Repositories;
using MySpot.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySpot.Application.Commands.Handlers
{
    internal sealed class ReserveParkingSpotForCleaningHandler : ICommandHandler<ReserveParkingSpotForCleaning>
    {
        private readonly IWeeklyParkingSpotRepository _repository;
        private readonly IParkingReservationService _reservationService;

        public ReserveParkingSpotForCleaningHandler(IWeeklyParkingSpotRepository weeklyParkingSpotRepository, IParkingReservationService parkingReservationService)
        {
            _repository = weeklyParkingSpotRepository;
            _reservationService = parkingReservationService;
        }

        public async Task HandleAsync(ReserveParkingSpotForCleaning command)
        {
            //TODO: date in current week checking
            var week = new Week(command.date);
            var weeklyParkingSpots = (await _repository.GetByWeekAsync(week)).ToList();

            _reservationService.ReserveParkingForCleaning(weeklyParkingSpots, new Date(command.date));

            //var tasks = weeklyParkingSpots.Select(x => _weeklyParkingSpotRepository.UdpadeAsync(x));
            //await Task.WhenAll(tasks);

            foreach (var parkingSpot in weeklyParkingSpots)
            {
                await _repository.UdpadeAsync(parkingSpot);
            }
        }
    }
}
