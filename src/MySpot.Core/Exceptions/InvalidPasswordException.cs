using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySpot.Core.Exceptions
{
    public sealed class InvalidPasswordException : CustomException
    {
        public InvalidPasswordException() : base("Invalid password.")
        {
        }
    }
}
