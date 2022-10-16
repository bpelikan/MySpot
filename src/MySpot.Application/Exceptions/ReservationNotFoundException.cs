using MySpot.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySpot.Application.Exceptions
{
    public sealed class ReservationNotFoundException : CustomException
    {
        public Guid Id { get; }

        public ReservationNotFoundException(Guid id)
            : base($"Reservation with ID: {id} was not found.")
        {
            Id = id;
        }
    }
}
