using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySpot.Core.Exceptions
{
    public class InvalidUsernameException : CustomException
    {
        public string UserName { get; }

        public InvalidUsernameException(string userName) : base($"Username: '{userName}' is invalid.")
        {
            UserName = userName;
        }
    }
}
