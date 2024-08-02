using Microsoft.EntityFrameworkCore; // Imports the Entity Framework Core namespace.
using TeamWebAPI.Controllers;
using TeamWebAPI.Models; // Imports namespace where models are defined.

namespace TeamWebAPI.Data
{
    // Defines the application's database context, inheriting from DbContext.
    public class AppDbContext : DbContext
    {
        // Constructor that tales DbContextOptions and passes them to the base DbContext constructor.
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){}

        // Defines a DbSet property for TeamMember entities, which maps to a database table.
        public DbSet<TeamMember> TeamMembers { get; set; }

        // Db set added for hobbies
        public DbSet<TeamHobbiesController> TeamHobbiesController { get; set; }

        // Add other DbSets here
        public DbSet<TrainSchedule> TrainSchedules { get; set;}

        // DB set added for BreakfastFood
        public DbSet<BreakfastFood> BreakfastFood { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("DefaultConnection");
            }
        }
    }
}
