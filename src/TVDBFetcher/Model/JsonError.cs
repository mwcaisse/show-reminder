using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TVDBFetcher.Model
{
    public class JsonError
    {

        public List<string> InvalidFilters { get; set; }

        public string InvalidLanguage { get; set; }

        public List<string> InvalidQueryParams { get; set; }

    }
}
