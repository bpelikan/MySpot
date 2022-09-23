using MySpot.Application.Commands;
using MySpot.Application.Services;
using MySpot.Tests.Unit.Shared;
using Shouldly;
using System;
using System.Linq;
using Xunit;
using MySpot.Core.Repositories;
using MySpot.Infrastructure.Repositories;

namespace MySpot.Tests.Unit.Services
{
    public class ReservationsServiceTests
    {
        [Fact]
        public void given_reservation_for_not_taken_date_create_reservation_should_succeed()
        {
            var weeklyParkingSpot = _weeklyParkingSpotsRepository.GetAll().First();
            var command = new CreateReservation(weeklyParkingSpot.Id, Guid.NewGuid(),
                DateTime.UtcNow.AddMinutes(5), "John Doe", "XYZ123");

            var reservationId = _reservationsService.Create(command);

            reservationId.ShouldNotBeNull();
            reservationId.Value.ShouldBe(command.ReservationId);
        }


        #region Arrange

        private readonly IClock _clock;
        private readonly IWeeklyParkingSpotRepository _weeklyParkingSpotsRepository;
        private readonly IReservationsService _reservationsService;

        public ReservationsServiceTests()
        { 
            _clock = new TestClock();
            _weeklyParkingSpotsRepository = new InMemoryWeeklyParkingSpotRepository(_clock);
            _reservationsService = new ReservationsService(_clock, _weeklyParkingSpotsRepository);
        }
        #endregion
    }
}
