using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySpot.Core.Exceptions
{
    public sealed class InvalidFullNameException : CustomException
    {
        public string FullName { get; }

        public InvalidFullNameException(string fullName) : base($"Full name: '{fullName}' is invalid.")
        {
            FullName = fullName;
        }
    }
}
