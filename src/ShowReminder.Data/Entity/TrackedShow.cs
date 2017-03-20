using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShowReminder.Data.Entity
{

    public class TrackedShow : BaseEntity
    {
        public long TvdbId { get; set; }

        public string Name { get; set; }

        public DateTime? FirstAiredDate { get; set; }

        public string AirDay { get; set; }

        public string AirTime { get; set; }

        public long? LastEpisodeId { get; set; }

        public long? NextEpisodeId { get; set; }

        public TrackedEpisode LastEpisode { get; set; }

        public TrackedEpisode NextEpisode { get; set; }

    }
}
