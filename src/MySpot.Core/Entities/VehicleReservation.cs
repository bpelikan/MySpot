using MySpot.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySpot.Core.Entities
{
    public sealed class VehicleReservation : Reservation
    {
        public UserId UserId { get; private set; }
        public EmployeeName EmployeeName { get; private set; }
        public LicensePlate LicensePlate { get; private set; }

        private VehicleReservation()
        {
        }

        public VehicleReservation(ReservationId reservationId, ParkingSpotId parkingSpotId, UserId userId, EmployeeName employeeName,
            LicensePlate licensePlate, Capacity capacity, Date date) : base(reservationId, parkingSpotId, capacity, date)
        {
            UserId = userId;
            EmployeeName = employeeName;
            ChangeLicencePlate(licensePlate);
        }

        public void ChangeLicencePlate(LicensePlate licensePlate)
            => LicensePlate = licensePlate;
    }
}
