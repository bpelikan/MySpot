using MySpot.Application.DTO;
using MySpot.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySpot.Infrastructure.DAL.Handlers
{
    internal static class Extensions
    {
        public static WeeklyParkingSpotDto AsDto(this WeeklyParkingSpot entity)
            => new WeeklyParkingSpotDto()
            {
                Id = entity.Id.ToString(),
                Name = entity.Name,
                Capacity = entity.Capacity,
                From = entity.Week.From.Value.DateTime,
                To = entity.Week.To.Value.DateTime,
                Reservations = entity.Reservations.Select(x => new ReservationDto
                {
                    Id = x.Id,
                    ParkingSpotId = x.ParkingSpotId,
                    EmployeeName = x is VehicleReservation vr ? vr.EmployeeName : null,
                    Type = x is VehicleReservation ? "vehicle" : "cleaning",
                    Date = x.Date.Value.Date
                })
            };

        public static UserDto AsDto(this User entity)
            => new()
            {
                Id = entity.Id,
                Username = entity.Username,
                FullName = entity.FullName
            };
    }
}
