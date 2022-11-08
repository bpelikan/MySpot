using MySpot.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySpot.Application.Exceptions
{
    internal class InvalidCredentialsException : CustomException
    {
        public InvalidCredentialsException() : base("Invalid credentials.")
        {
        }
    }
}
