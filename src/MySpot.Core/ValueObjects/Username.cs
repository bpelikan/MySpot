﻿using MySpot.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySpot.Core.ValueObjects
{
    public sealed record Username
    {
        public string Value { get; }

        public Username(string value)
        {
            if (string.IsNullOrWhiteSpace(value) || value.Length is > 30 or < 3)
            {
                throw new InvalidUsernameException(value);
            }

            Value = value;
        }

        public static implicit operator Username(string value) => new Username(value);

        public static implicit operator string(Username value) => value?.Value;

        public override string ToString() => Value;
    }
}
