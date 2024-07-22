using System;

namespace TeamWebAPI.Models
{
    public class TeamMember
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public DateTime Birthday { get; set; }
        public string CollegeProgram { get; set; }
        public string YearInProgram { get; set; }
    }
}
