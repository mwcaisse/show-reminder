using System;
using System.Collections.Generic;
using System.Text;

namespace ShowReminder.TMDBFetcher.Model.Search
{
    public class SearchShow
    {

        public string PosterPath { get; set; }

        public double Popularity { get; set; }

        public long Id { get; set; }

        public string BackdropPath { get; set; }

        public double VoteAverage { get; set; }

        public string Overview { get; set; }

        public DateTime FirstAirDate { get; set; }

        public string[] OriginCountry { get; set; }

        public string[] GenreIds { get; set; }

        public string OriginalLanguage { get; set; }

        public int VoteCount { get; set; }

        public string Name { get; set; }

        public string OriginalName { get; set; }

    }
}
