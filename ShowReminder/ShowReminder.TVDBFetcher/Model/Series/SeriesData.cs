using System.Collections.Generic;

namespace ShowReminder.TVDBFetcher.Model.Series
{
    public class SeriesData
    {

        public Series Data { get; set; }

        public List<JsonError> Errors { get; set; }
        
    }
}
