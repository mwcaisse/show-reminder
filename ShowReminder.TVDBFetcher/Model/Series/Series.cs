using System;
using System.Collections.Generic;

namespace ShowReminder.TVDBFetcher.Model.Series
{
    public class Series
    {

        public int Id { get; set; }

        public string SeriesName { get; set; }

        public List<string> Aliases { get; set; }

        public string Banner { get; set; }

        public int? SeriesId { get; set; }

        public string Status { get; set; }

        public DateTime? FirstAired { get; set; }

        public string Network { get; set; }

        public string NetworkId { get; set; }

        public string Runtime { get; set; }

        public List<string> Genre { get; set; }

        public string Overview { get; set; }

        public int LastUpdated { get; set; }

        public string AirsDayOfWeek { get; set; }

        public string AirsTime { get; set; }

        public string Rating { get; set; }

        public string ImdbId { get; set; }

        public string Zap2ItId { get; set; }

        public DateTime? Added { get; set; }

        public string SiteRating { get; set; }

        public string SiteRatingCount { get; set; }

    }
}
