using MySpot.Api.Commands;
using MySpot.Api.Entities;
using MySpot.Api.Services;
using MySpot.Api.ValueObjects;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MySpot.Tests.Unit.Services
{
    public class ReservationsServiceTests
    {
        [Fact]
        public void given_reservation_for_not_taken_date_create_reservation_should_succeed()
        {
            var weeklyParkingSpot = _weeklyParkingSpots.First();
            var command = new CreateReservation(weeklyParkingSpot.Id, Guid.NewGuid(),
                DateTime.UtcNow.AddMinutes(5), "John Doe", "XYZ123");

            var reservationId = _reservationsService.Create(command);

            reservationId.ShouldNotBeNull();
            reservationId.Value.ShouldBe(command.ReservationId);
        }


        #region Arrange

        private readonly Clock _clock = new();

        private readonly ReservationsService _reservationsService;
        private readonly List<WeeklyParkingSpot> _weeklyParkingSpots;

        public ReservationsServiceTests()
        {
            _weeklyParkingSpots = new()
            {
                new WeeklyParkingSpot(Guid.Parse("00000000-0000-0000-0000-000000000001"), new Week(_clock.Current()), "P1"),
                new WeeklyParkingSpot(Guid.Parse("00000000-0000-0000-0000-000000000002"), new Week(_clock.Current()), "P2"),
                new WeeklyParkingSpot(Guid.Parse("00000000-0000-0000-0000-000000000003"), new Week(_clock.Current()), "P3"),
                new WeeklyParkingSpot(Guid.Parse("00000000-0000-0000-0000-000000000004"), new Week(_clock.Current()), "P4"),
                new WeeklyParkingSpot(Guid.Parse("00000000-0000-0000-0000-000000000005"), new Week(_clock.Current()), "P5"),
            };

            _reservationsService = new ReservationsService(_clock, _weeklyParkingSpots);
        }
        #endregion
    }
}
