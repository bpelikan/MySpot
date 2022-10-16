using MySpot.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySpot.Core.Exceptions
{
    internal sealed class NoReservationPolicyFoundException : CustomException
    {
        public JobTitle JobTitle { get; }


        public NoReservationPolicyFoundException(JobTitle jobTitle) 
            : base($"No reservation for {jobTitle} has been found.")
        {
            JobTitle = jobTitle;
        }

    }
}
