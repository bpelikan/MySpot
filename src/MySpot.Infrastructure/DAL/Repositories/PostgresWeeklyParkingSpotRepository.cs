using Microsoft.EntityFrameworkCore;
using MySpot.Core.Entities;
using MySpot.Core.Repositories;
using MySpot.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySpot.Infrastructure.DAL.Repositories
{
    internal sealed class PostgresWeeklyParkingSpotRepository : IWeeklyParkingSpotRepository
    {
        private readonly MySpotDbContext _dbContext;

        public PostgresWeeklyParkingSpotRepository(MySpotDbContext mySpotDbContext)
        {
            _dbContext = mySpotDbContext;
        }

        public WeeklyParkingSpot Get(ParkingSpotId id)
            => _dbContext.WeeklyParkingSpots
                .Include(x => x.Reservations)
                .SingleOrDefault(x => x.Id == id);

        public IEnumerable<WeeklyParkingSpot> GetAll()
            => _dbContext.WeeklyParkingSpots
                .Include(x => x.Reservations)
                .ToList();

        public void Create(WeeklyParkingSpot weeklyParkingSpot)
        {
            _dbContext.Add(weeklyParkingSpot);
            _dbContext.SaveChanges();
        }

        public void Udpade(WeeklyParkingSpot weeklyParkingSpot)
        {
            _dbContext.Update(weeklyParkingSpot);
            _dbContext.SaveChanges();
        }

        public void Delete(WeeklyParkingSpot weeklyParkingSpot)
        {
            _dbContext.Remove(weeklyParkingSpot);
            _dbContext.SaveChanges();
        }
    }
}
