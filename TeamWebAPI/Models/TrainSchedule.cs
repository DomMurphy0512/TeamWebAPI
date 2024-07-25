using System;

namespace TeamWebAPI.Models
{
    public class TrainSchedule
    {
        public int Id { get; set; }

        public string  DepartureCity { get; set; } = string.Empty;

        public DateTime DepartureTime { get; set; }
        public string DestinationCity { get; set; } = string.Empty;
        public int TripLength { get; set; }
    }
}
