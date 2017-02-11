using System.Collections.Generic;

namespace ShowReminder.TVDBFetcher.Model
{
    public class SeriesData
    {

        public Series Data { get; set; }

        public List<JsonError> Errors { get; set; }

        public string Sparta { get; set; }

        public SeriesData()
        {
            Sparta = "Mitchell";
        }

    }
}
