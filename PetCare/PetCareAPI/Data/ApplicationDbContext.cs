using Microsoft.EntityFrameworkCore;
using PetCareAPI.Models;

namespace PetCareAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }
        public DbSet<Pet> Pet { get; set; } //Table Name = Pet
        public DbSet<Food> Food { get; set; }
        public DbSet<Activity> Activity { get; set; }
        public DbSet<HealthStatus> HealthStatus { get; set; }
        public DbSet<User> User { get; set; }



    }
}
