using System;
using System.Collections.Generic;
using System.Text;
using ShowReminder.TMDBFetcher.Model;
using ShowReminder.TMDBFetcher.Model.Search;

namespace ShowReminder.TMDBFetcher.Manager
{
    public class ShowManager : AbstractManager
    {

        public ShowManager(TMDBSettings settings) : base(settings)
        {
            
        }

        public SearchResult Search(string query, int page = 1)
        {
            var url = BaseUrl + "search/tv?language=en-US&query=" + query + "&page=" + page + "&include_adult=true";
            return GetRequest<SearchResult>(url);
        }

    }
}
