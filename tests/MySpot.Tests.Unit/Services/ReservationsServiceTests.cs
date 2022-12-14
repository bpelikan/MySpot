using MySpot.Application.Commands;
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
using MySpot.Core.Policies;

namespace MySpot.Tests.Unit.Services
{
    public class ReservationsServiceTests
    {
        [Fact]
        public async Task given_reservation_for_not_taken_date_create_reservation_should_succeed()
        {
            var weeklyParkingSpot = (await _weeklyParkingSpotsRepository.GetAllAsync()).First();
            var command = new ReserveParkingSpotForVehicle(weeklyParkingSpot.Id, Guid.NewGuid(),
               1,  _now.AddMinutes(5), "John Doe", "XYZ123");

            var reservationId = await _reservationsService.ReserveForVehicleAsync(command);

            reservationId.ShouldNotBeNull();
            reservationId.Value.ShouldBe(command.ReservationId);
        }


        #region Arrange

        private readonly IClock _clock;
        private readonly IWeeklyParkingSpotRepository _weeklyParkingSpotsRepository;
        private readonly IReservationsService _reservationsService;
        private readonly DateTime _now;

        public ReservationsServiceTests()
        {
            _now = DateTime.Parse("2022-09-22");
            _clock = new TestClock();
            _weeklyParkingSpotsRepository = new InMemoryWeeklyParkingSpotRepository(_clock);

            var parkingReservationService = new ParkingReservationService(new IReservationPolicy[]
            {
                new BossReservationPolicy(),
                new ManagerReservationPolicy(),
                new RegularEmployeeReservationPolicy(_clock)
            },
            _clock);

            _reservationsService = new ReservationsService(_clock, _weeklyParkingSpotsRepository, parkingReservationService);
        }
        #endregion
    }
}
