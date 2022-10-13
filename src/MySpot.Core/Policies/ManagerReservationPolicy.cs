using MySpot.Core.Abstractions;
using MySpot.Core.Entities;
using MySpot.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySpot.Core.Policies
{
    internal sealed class ManagerReservationPolicy : IReservationPolicy
    {
        public bool CanBeApplied(JobTitle jobTitle)
            => jobTitle == JobTitle.Manager;

        public bool CanReserve(IEnumerable<WeeklyParkingSpot> weeklyParkingSpots, EmployeeName employeeName)
        {
            var totalEmployeeReservations = weeklyParkingSpots
                .SelectMany(x => x.Reservations)
                .OfType<VehicleReservation>()
                .Count(x => x.EmployeeName == employeeName);

            return totalEmployeeReservations <= 4;
        }
    }
}
