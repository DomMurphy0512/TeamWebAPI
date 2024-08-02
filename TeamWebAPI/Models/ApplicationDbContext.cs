using Microsoft.EntityFrameworkCore;
using TeamWebAPI.Models;
namespace TeamWebAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Hobby> Hobbies { get; set; }
        // Define other DbSet properties
    }
}
