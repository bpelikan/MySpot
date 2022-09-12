namespace MySpot.Api.Services
{
    public class Clock  //why not static?
    {
        public DateTime Current() => DateTime.UtcNow;
    }
}