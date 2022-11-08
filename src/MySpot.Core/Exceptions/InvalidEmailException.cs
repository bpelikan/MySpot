using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySpot.Core.Exceptions
{
    public sealed class InvalidEmailException : CustomException
    {
        public string Email { get; }

        public InvalidEmailException(string email) : base($"Email: '{email}' is invalid.")
        {
            Email = email;
        }
    }
}
