using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySpot.Core.Exceptions
{
    public sealed class InvalidRoleException : CustomException
    {
        public string Role { get; }

        public InvalidRoleException(string role) : base($"Role: '{role}' is invalid.")
        {
            Role = role;
        }
    }
}
