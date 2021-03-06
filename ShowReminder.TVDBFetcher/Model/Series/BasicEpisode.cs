﻿using System;

namespace ShowReminder.TVDBFetcher.Model.Series
{
    public class BasicEpisode
    {

        public int? AbsoluteNumber { get; set; }

        public int? AiredEpisodeNumber { get; set; }

        public int? AiredSeason { get; set; }

        public string DvdEpisodeNumber { get; set; }

        public string DvdSeason { get; set; }

        public string EpisodeName { get; set; }

        public int Id { get; set; }

        public string Overview { get; set; }

        public DateTime? FirstAired { get; set; }

        public int LastUpdated { get; set; }

    }
}
