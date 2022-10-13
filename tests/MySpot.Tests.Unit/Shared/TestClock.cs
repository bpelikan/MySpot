using System;
using MySpot.Application.Services;
using MySpot.Core.Abstractions;

namespace MySpot.Tests.Unit.Shared
{
    public class TestClock : IClock
    {
        public DateTime Current() => new(2022,09,21, 12, 0, 0);
    }
}