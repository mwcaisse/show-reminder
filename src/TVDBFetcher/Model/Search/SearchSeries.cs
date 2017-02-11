using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShowReminder.TVDBFetcher.Model.Search
{
    public class SearchSeries
    {

        public List<string> Aliases { get; set; }

        public string Banner { get; set; }

        public string FirstAired { get; set; }

        public int Id { get; set; }

        public string Network { get; set; }

        public string Overview { get; set; }

        public string SeriesName { get; set; }

        public string Status { get; set; }

    }
}
