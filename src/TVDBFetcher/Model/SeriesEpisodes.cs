using System.Collections.Generic;

namespace ShowReminder.TVDBFetcher.Model
{
    public class SeriesEpisodes
    {

        public Links Links { get; set; }

        public List<BasicEpisode> Data { get; set; }

        public List<JsonError> Errors { get; set; }

    }
}
