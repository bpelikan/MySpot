using MySpot.Api.Entities;
using MySpot.Api.Exceptions;
using MySpot.Api.ValueObjects;
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
            var reservation = new Reservation(Guid.NewGuid(), _weeklyParkingSpot.Id, "John Doe",
                "XYZ123", new Date(invalidDate));

            //act
            var exception = Record.Exception(() => _weeklyParkingSpot.AddReservation(reservation, _now));

            //assert
            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<InvalidReservationDateException>();
        }

        [Fact]
        public void given_reservation_for_already_exising_date_add_reservation_should_fail() 
        {
            var reservationDate = _now.AddDays(1);
            var reservation = new Reservation(Guid.NewGuid(), _weeklyParkingSpot.Id, "John Doe", "XYZ123", new Date(reservationDate));
            var nextReservation = new Reservation(Guid.NewGuid(), _weeklyParkingSpot.Id, "John Doe", "XYZ123", new Date(reservationDate));
            _weeklyParkingSpot.AddReservation(reservation, _now);

            var exception = Record.Exception(() => _weeklyParkingSpot.AddReservation(nextReservation, reservationDate));

            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<ParkingSpotAlreadyReservedException>();
        }

        [Fact]
        public void given_reservation_for_not_taken_date_add_reservation_should_succeed()
        {
            var reservationDate = _now.AddDays(1);
            var reservation = new Reservation(Guid.NewGuid(), _weeklyParkingSpot.Id, "John Doe", "XYZ123", new Date(reservationDate));
            
            _weeklyParkingSpot.AddReservation(reservation, _now);

            _weeklyParkingSpot.Reservations.ShouldHaveSingleItem();
        }


        #region Arrange
        private readonly Date _now;
        private readonly WeeklyParkingSpot _weeklyParkingSpot;


        public WeeklyParkingSpotTests()
        {
            _now = new Date(new DateTime(2022, 08, 10));
            _weeklyParkingSpot = new WeeklyParkingSpot(Guid.NewGuid(), new Week(_now), "P1");
        }
        #endregion


    }
}