using System;
using MySpot.Api.Services;

namespace MySpot.Tests.Unit.Shared
{
    public class TestClock : IClock
    {
        public DateTime Current() => new(2022,09,21);
    }
}