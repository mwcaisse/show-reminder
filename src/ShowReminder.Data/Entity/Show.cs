using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShowReminder.Data.Entity
{
    public class Show : BaseEntity
    {
        public long TvdbId { get; set; }

        public string Name { get; set; }

        public DateTime FirstAiredDate { get; set; }

        public string AirDay { get; set; }

        public string AirTime { get; set; }

        public DateTime LastEpisodeDate { get; set; }

        public DateTime NextEpisodeDate { get; set; }

    }
}
