using MySpot.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySpot.Core.Exceptions
{
    internal sealed class CannotReserveParkingSpotException : CustomException
    {
        public ParkingSpotId ParkingSpotId { get; }


        public CannotReserveParkingSpotException(ParkingSpotId parkingSpotId) 
            : base($"Cannot reserve parking spot with ID: {parkingSpotId}")
        {
            ParkingSpotId = parkingSpotId;
        }

    }
}
