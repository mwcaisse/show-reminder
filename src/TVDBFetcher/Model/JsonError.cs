using System.Collections.Generic;

namespace ShowReminder.TVDBFetcher.Model
{
    public class JsonError
    {

        public List<string> InvalidFilters { get; set; }

        public string InvalidLanguage { get; set; }

        public List<string> InvalidQueryParams { get; set; }

    }
}
