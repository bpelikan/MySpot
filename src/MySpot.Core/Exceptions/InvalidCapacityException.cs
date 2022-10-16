using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySpot.Core.Exceptions
{
    internal sealed class InvalidCapacityException : CustomException
    {
        public int Capacity { get; }


        public InvalidCapacityException(int capacity) 
            : base($"Capacity {capacity} is invalid.")
        {
            Capacity = capacity;
        }

    }
}
