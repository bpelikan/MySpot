using MySpot.Core.Entities;
using MySpot.Core.ValueObjects;

namespace MySpot.Core.Repositories
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