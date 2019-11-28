using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShowReminder.TVDBFetcher.Model.Authentication;
using ShowReminder.TVDBFetcher.Model.Search;

namespace ShowReminder.TVDBFetcher.Manager
{
    public class SearchManager : AbstractManager 
    {
        public SearchManager(AuthenticationParam authParam) : base(authParam)
        {
        }

        public SearchResult SearchByName(string name)
        {
            var url = BaseUrl + "search/series?name=" + name;
            return GetRequest<SearchResult>(url);
        }

    }
}
