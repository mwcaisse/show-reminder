using System;
using System.Collections.Generic;
using System.Text;
using ShowReminder.TMDBFetcher.Model.TV;

namespace ShowReminder.TMDBFetcher.Model.Search
{
    public class SearchResult
    {
        public int Page { get; set; }

        public List<TVShow> Results { get; set; }

        public int TotalResults { get; set; }

        public int TotalPages { get; set; }

    }
}
