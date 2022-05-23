using ParkyAPI.Data;
using ParkyAPI.Models;
using ParkyAPI.Repository.IRepository;
using System.Collections.Generic;
using System.Linq;

namespace ParkyAPI.Repository
{
    public class NationalParkRepository : INationalParkRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public NationalParkRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public bool CreateNationalPark(NationalPark nationalPark)
        {
            _dbContext.NationalParks.Add(nationalPark);
            return Save();
        }

        public bool DeleteNationalPark(NationalPark nationalPark)
        {
            _dbContext.NationalParks.Remove(nationalPark);
            return Save();
        }

        public ICollection<NationalPark> GetNationalPark()
        {
            return _dbContext.NationalParks.OrderBy(a => a.Name).ToList();
        }

        public NationalPark GetNationalPark(int nationalParkId)
        {
            return _dbContext.NationalParks.FirstOrDefault(a => a.Id == nationalParkId);
        }

        public bool NationalParkExist(string name)
        {
            bool value = _dbContext.NationalParks.Any(x => x.Name.ToLower().Trim() == name.ToLower().Trim());
            return value;
        }

        public bool NationalParkExists(int id)
        {
            return _dbContext.NationalParks.Any(a => a.Id == id);
        }

        public bool Save()
        {
            return _dbContext.SaveChanges() > 0 ? true : false;
        }

        public bool UpdateeNationalPark(NationalPark nationalPark)
        {
            _dbContext.NationalParks.Update(nationalPark);
            return Save();
        }
    }
}
