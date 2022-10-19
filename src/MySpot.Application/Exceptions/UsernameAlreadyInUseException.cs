using MySpot.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySpot.Application.Exceptions
{
    public sealed class UsernameAlreadyInUseException : CustomException
    {
        public string Username { get; }

        public UsernameAlreadyInUseException(string username) : base($"Username: '{username}' is already in use.")
        {
            Username = username;
        }
    }
}
