using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TVDBFetcher.Model
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
