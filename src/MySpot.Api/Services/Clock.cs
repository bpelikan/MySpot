namespace MySpot.Api.Services
{
    public class Clock : IClock  //why not static?
    {
        public DateTime Current() => DateTime.UtcNow;
    }
}