namespace MySpot.Application.Services
{
    public class Clock : IClock  //why not static?
    {
        public DateTime Current() => DateTime.UtcNow;
    }
}