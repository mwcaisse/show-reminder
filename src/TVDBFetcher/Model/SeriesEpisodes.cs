using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TVDBFetcher.Model
{
    public class SeriesEpisodes
    {

        public Links Links { get; set; }

        public List<BasicEpisode> Data { get; set; }

        public List<JsonError> Errors { get; set; }

    }
}
