using MySpot.Core.Entities;
using MySpot.Core.Exceptions;
using MySpot.Core.ValueObjects;
using Shouldly;
using System;
using System.Runtime.InteropServices;
using Xunit;

namespace MySpot.Tests.Unit.Entities
{
    public class WeeklyParkingSpotTests
    {
        [Theory]
        [InlineData("2022-09-09")]
        [InlineData("2022-09-24")]
        public void given_invalid_date_add_reservation_should_fail(string dateString)
        {
            //arange
            var invalidDate = DateTime.Parse(dateString);
            var reservation = new VehicleReservation(Guid.NewGuid(), _weeklyParkingSpot.Id, 
                "John Doe", "XYZ123", 1, new Date(invalidDate));

            //act
            var exception = Record.Exception(() => _weeklyParkingSpot.AddReservation(reservation, _now));

            //assert
            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<InvalidReservationDateException>();
        }

        [Fact]
        public void given_reservation_for_already_reserved_parking_spot_add_reservation_should_fail() 
        {
            var reservationDate = _now.AddDays(1);
            var reservation = new VehicleReservation(Guid.NewGuid(), _weeklyParkingSpot.Id, 
                "John Doe", "XYZ123", 2, new Date(reservationDate));
            var nextReservation = new VehicleReservation(Guid.NewGuid(), _weeklyParkingSpot.Id, 
                "John Doe", "XYZ123", 1, new Date(reservationDate));
            _weeklyParkingSpot.AddReservation(reservation, _now);

            var exception = Record.Exception(() => _weeklyParkingSpot.AddReservation(nextReservation, reservationDate));

            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<ParkingSpotCapacityExceededException>();
        }

        [Fact]
        public void given_reservation_for_not_reserved_parking_spot_add_reservation_should_succeed()
        {
            var reservationDate = _now.AddDays(1);
            var reservation = new VehicleReservation(Guid.NewGuid(), _weeklyParkingSpot.Id, 
                "John Doe", "XYZ123", 1, new Date(reservationDate));
            
            _weeklyParkingSpot.AddReservation(reservation, _now);

            _weeklyParkingSpot.Reservations.ShouldHaveSingleItem();
        }


        #region Arrange
        private readonly Date _now;
        private readonly WeeklyParkingSpot _weeklyParkingSpot;


        public WeeklyParkingSpotTests()
        {
            _now = new Date(new DateTime(2022, 08, 10));
            _weeklyParkingSpot = WeeklyParkingSpot.Create(Guid.NewGuid(), new Week(_now), "P1");
        }
        #endregion


    }
}