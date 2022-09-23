using MySpot.Application.Services;

namespace MySpot.Infrastructure.Services
{
    public class Clock : IClock  //why not static?
    {
        public DateTime Current() => DateTime.UtcNow;
    }
}