using MySpot.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySpot.Core.ValueObjects
{
    public sealed record class Capacity
    {
        public int Value { get; set; }

        public Capacity(int value)
        {
            if (value is < 0 or > 4)
                throw new InvalidCapacityException(value);
            Value = value;
        }

        public static implicit operator int(Capacity capacity)
            => capacity.Value;

        public static implicit operator Capacity(int value)
            => new(value);
    }
}
