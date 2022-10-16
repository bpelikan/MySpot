using MySpot.Core.Abstractions;

namespace MySpot.Infrastructure.Services
{
    public class Clock : IClock  //why not static?
    {
        public DateTime Current() => DateTime.UtcNow;
    }
}