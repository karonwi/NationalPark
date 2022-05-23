using ParkyAPI.Models;
using System.Collections.Generic;

namespace ParkyAPI.Repository.IRepository
{
    public interface ITrailRepository
    {
        ICollection<Trail> GetTrail();
        ICollection<Trail> GetTrailsInNationalPark(int npId);

        Trail GetTrail(int trailId);
        bool TrailExist(string name);
        bool TrailExists(int id);
        bool CreateTrail(Trail trail);
        bool UpdateeTrail(Trail trail);
        bool DeleteTrail(Trail trail);
        bool Save();
    }
}
