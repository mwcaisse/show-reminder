using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShowReminder.Data.Entity
{
    public class TrackedEpisode : BaseEntity
    {

        public int? OverallNumber { get; set; }

        public int? SeasonNumber { get; set; }

        public int? EpisodeNumber { get; set; }

        public DateTime? AirDate { get; set; }

        public string Name { get; set; }

        public string Overview { get; set; }

    }
}
