using System;
using System.Collections.Generic;
using System.Text;

namespace ShowReminder.TMDBFetcher.Model.TV
{
    public class TVShow
    {

        public string BackdropPath { get; set; }

        public int[] EpisodeRunTime { get; set; }

        public DateTime FirstAirDate { get; set; }

        public string Homepage { get; set; }

        public int Id { get; set; }

        public bool InProducation { get; set; }

        public string[] Languages { get; set; }

        public DateTime? LastAirDate { get; set; }

        public string Name { get; set; }

        public int NumberOfEpisodes { get; set; }
        
        public int NumberOfSeasons { get; set; }

        public string[] OriginCountry { get; set; }

        public string OriginalLanguage { get; set; }

        public string OriginalName { get; set; }

        public string Overview { get; set; }

        public double Popularity { get; set; }

        public string PosterPath { get; set; }

        public List<TVSeason> Seasons { get; set; }

        public string Status { get; set; }

        public string Type { get; set; }

        public double VoteAverage { get; set; }

        public int VoteCount { get; set; }

    }
}
