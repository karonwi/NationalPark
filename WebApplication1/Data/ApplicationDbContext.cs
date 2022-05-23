using Microsoft.EntityFrameworkCore;
using ParkyAPI.Models;

namespace ParkyAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> opitons) : base (opitons)
        {
             
        }

        public DbSet<NationalPark> NationalParks { get; set; }
        public DbSet<Trail> Trails { get; set; }
    }
}
