using System; // Imports namespace to handle DateTime.

namespace TeamWebAPI.Models
{
    // Defines the TeamMember class which represents a team member entity.
    public class TeamMember
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public DateTime Birthday { get; set; }
        public string CollegeProgram { get; set; }
        public string YearInProgram { get; set; }
    }
}
