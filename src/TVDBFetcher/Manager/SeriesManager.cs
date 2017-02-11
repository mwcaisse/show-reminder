using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using ShowReminder.TVDBFetcher.Model;
using ShowReminder.TVDBFetcher.Model.Authentication;

namespace ShowReminder.TVDBFetcher.Manager
{
    public class SeriesManager : AbstractManager
    {
        public SeriesManager(AuthenticationParam authParam) : base(authParam)
        {
        }

        public BasicEpisode GetNextAiringEpisode(int showId)
        {

            return null;
        }

        public SeriesData GetSeries(int id)
        {
            var url = "https://api.thetvdb.com/series/" + id;
            return GetRequest<SeriesData>(url);
        }
    }
}
