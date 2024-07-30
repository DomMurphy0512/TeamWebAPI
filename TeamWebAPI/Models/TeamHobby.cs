using System; 

namespace TeamWebAPI.Models
{
    // Name Hobby class and which part reperesent what
    public class Hobby
    {
        public int Id { get; set; }
        public string HobbyName { get; set; } = string.Empty;
        public int YearsOfExperience { get; set; }
        public string Description { get; set; } = string.Empty;
        public int UserId { get; set; }
    }
}
