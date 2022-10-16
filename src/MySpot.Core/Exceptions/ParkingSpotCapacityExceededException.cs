using MySpot.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySpot.Core.Exceptions
{
    internal sealed class ParkingSpotCapacityExceededException : CustomException
    {
        public ParkingSpotId ParkingSpotId { get; }

        public ParkingSpotCapacityExceededException(ParkingSpotId parkingSpotId) : base($"Parking spot with ID: {parkingSpotId} exceeds its reservation capacity.")
        {
            ParkingSpotId = parkingSpotId;
        }

    }
}
