using MySpot.Application.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySpot.Application.Commands
{
    public record SignIn(string Email, string Password) : ICommand;
}
