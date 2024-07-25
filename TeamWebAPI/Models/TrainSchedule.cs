using System;

namespace TeamWebAPI.Models
{
    public class TrainSchedule
    {
        public int Id { get; set; }

        public string  DepartureCity { get; set; }

        public DateTime DepartureTime { get; set; }
        public string DestinationCity { get; set; }
        public int TripLength { get; set; }
    }
}
