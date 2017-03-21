using System;
using System.Collections.Generic;
using System.Text;

namespace ShowReminder.TMDBFetcher.Model.Search
{
    public class SearchResult
    {
        public int Page { get; set; }

        public List<SearchShow> Results { get; set; }

        public int TotalResults { get; set; }

        public int TotalPages { get; set; }

    }
}
