using MySpot.Application.Commands;
using MySpot.Application.Services;
using MySpot.Tests.Unit.Shared;
using Shouldly;
using System;
using System.Linq;
using Xunit;
using MySpot.Core.Repositories;
using MySpot.Infrastructure.DAL.Repositories;
using System.Threading.Tasks;
using MySpot.Core.ValueObjects;
using MySpot.Core.Abstractions;
using MySpot.Core.DomainServices;

namespace MySpot.Tests.Unit.Services
{
    public class ReservationsServiceTests
    {
        [Fact]
        public async Task given_reservation_for_not_taken_date_create_reservation_should_succeed()
        {
            var weeklyParkingSpot = (await _weeklyParkingSpotsRepository.GetAllAsync()).First();
            var command = new CreateReservation(weeklyParkingSpot.Id, Guid.NewGuid(),
                _now.AddMinutes(5), "John Doe", "XYZ123");

            var reservationId = await _reservationsService.CreateAsync(command);

            reservationId.ShouldNotBeNull();
            reservationId.Value.ShouldBe(command.ReservationId);
        }


        #region Arrange

        private readonly IClock _clock;
        private readonly IWeeklyParkingSpotRepository _weeklyParkingSpotsRepository;
        private readonly IReservationsService _reservationsService;
        //private readonly IParkingReservationService _parkingReservationService;
        private readonly DateTime _now;

        public ReservationsServiceTests()
        {
            _now = DateTime.Parse("2022-09-22");
            _clock = new TestClock();
            _weeklyParkingSpotsRepository = new InMemoryWeeklyParkingSpotRepository(_clock);
            _reservationsService = new ReservationsService(_clock, _weeklyParkingSpotsRepository);
            //_reservationsService = new ReservationsService(_clock, _weeklyParkingSpotsRepository, _parkingReservationService);
        }
        #endregion
    }
}
