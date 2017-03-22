using System;
using System.Collections.Generic;
using System.Text;

namespace ShowReminder.TMDBFetcher.Model.TV
{
    public class TVSeason
    {

        public int Id { get; set; }

        public DateTime AirDate { get; set; }

        public List<TVEpisode> Episodes { get; set; }

        public string Name { get; set; }

        public string Overview { get; set; }

        public string PosterPath { get; set; }

        public int SeasonNumber { get; set; }

    }
}
