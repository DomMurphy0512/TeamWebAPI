using Microsoft.EntityFrameworkCore; // Imports the Entity Framework Core namespace.
using TeamWebAPI.Models; // Imports namespace where models are defined.

namespace TeamWebAPI.Data
{
    // Defines the application's database context, inheriting from DbContext.
    public class AppDbContext : DbContext
    {
        // Constructor that tales DbContextOptions and passes them to the base DbContext constructor.
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // Defines a DbSet property for TeamMember entities, which maps to a database table.
        public DbSet<TeamMember> TeamMembers { get; set; }
        // Add other DbSets here
        public DbSet<TrainSchedule> TrainSchedules { get; set;}
    }
}
