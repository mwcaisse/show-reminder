using System;
using System.Collections.Generic;
using System.Text;

namespace ShowReminder.TMDBFetcher.Model.TV
{
    public class TVEpisode
    {

        public DateTime AirDate { get; set; }

        public int EpisodeNumber { get; set; }

        public string Name { get; set; }

        public string Overview { get; set; }

        public int Id { get; set; }

        public string ProductionCode { get; set; }

        public int SeasonNumber { get; set; }

        public string StillPath { get; set; }

        public double VoteAverage { get; set; }

        public int VoteCount { get; set; }

    }
}
