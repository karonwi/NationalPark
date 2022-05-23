using Microsoft.EntityFrameworkCore;
using ParkyAPI.Data;
using ParkyAPI.Models;
using ParkyAPI.Repository.IRepository;
using System.Collections.Generic;
using System.Linq;

namespace ParkyAPI.Repository
{
    public class TrailRepository : ITrailRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public TrailRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public bool CreateTrail(Trail trail)
        {
            _dbContext.Trails.Add(trail);
            return Save();
        }

        public bool DeleteTrail(Trail trail)
        {
            _dbContext.Trails.Remove(trail);
            return Save();
        }

        public ICollection<Trail> GetTrail()
        {
            return _dbContext.Trails.Include(c => c.NationalPark).OrderBy(a => a.Name).ToList();
        }

        public Trail GetTrail(int trailId)
        {
            return _dbContext.Trails.Include(c => c.NationalPark).FirstOrDefault(a => a.Id == trailId);
        }

        public bool TrailExist(string name)
        {
            bool value = _dbContext.Trails.Any(x => x.Name.ToLower().Trim() == name.ToLower().Trim());
            return value;
        }

        public bool TrailExists(int id)
        {
            return _dbContext.Trails.Any(a => a.Id == id);
        }

        public bool Save()
        {
            return _dbContext.SaveChanges() > 0 ? true : false;
        }

        public bool UpdateeTrail(Trail trail)
        {
            _dbContext.Trails.Update(trail);
            return Save();
        }

        public ICollection<Trail> GetTrailsInNationalPark(int npId)
        {
            return _dbContext.Trails.Include(c => c.NationalPark).Where(c => c.NationalParkId == npId).ToList();
        }
    }
}
