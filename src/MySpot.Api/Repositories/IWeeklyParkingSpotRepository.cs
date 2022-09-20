using MySpot.Api.Entities;
using MySpot.Api.ValueObjects;

namespace MySpot.Api.Repositories
{
    public interface IWeeklyParkingSpotRepository
    {
        IEnumerable<WeeklyParkingSpot> GetAll();
        WeeklyParkingSpot Get(ParkingSpotId id);
        void Create(WeeklyParkingSpot weeklyParkingSpot);
        void Udpade(WeeklyParkingSpot weeklyParkingSpot);
        void Delete (WeeklyParkingSpot weeklyParkingSpot);
    }
}