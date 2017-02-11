using System;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using ShowReminder.TVDBFetcher.Model;
using ShowReminder.TVDBFetcher.Model.Authentication;
using System.Collections.Generic;
using System.Linq;

namespace ShowReminder.TVDBFetcher.Manager
{
    public class SeriesManager : AbstractManager
    {
        public SeriesManager(AuthenticationParam authParam) : base(authParam)
        {
        }

     

        public SeriesData GetSeries(int id)
        {
            var url = BaseUrl + $"series/{id}";
            return GetRequest<SeriesData>(url);
        }

        public SeriesEpisodes GetSeriesEpisodes(int id, string page = "")
        {
            var url = BaseUrl + $"series/{id}/episodes";
            if (!string.IsNullOrWhiteSpace(page))
            {
                url += $"?page={page}";
            }
            return GetRequest<SeriesEpisodes>(url);
        }

        public List<BasicEpisode> GetAllSeriesEpisodes(int id)
        {
            int page = 1;

            var seriesEpisodes = GetSeriesEpisodes(id, page + "");

            if (seriesEpisodes?.Links.Last != null)
            {
                int lastPage = seriesEpisodes.Links.Last.Value;
                List<BasicEpisode> episodes = new List<BasicEpisode>();
                episodes.AddRange(seriesEpisodes.Data);

                for (int i = page + 1; i <= lastPage; i++)
                {
                    seriesEpisodes = GetSeriesEpisodes(id, i + "");
                    episodes.AddRange(seriesEpisodes.Data);
                }

                return episodes.OrderBy(x => x.AbsoluteNumber).ToList();
            }

            return null;
        }

        public BasicEpisode GetNextAiringEpisode(int id)
        {
            var nextEpisode =
                GetAllSeriesEpisodes(id)
                    .OrderBy(x => x.FirstAired)
                    .FirstOrDefault(x => x.FirstAired.HasValue && x.FirstAired.Value > DateTime.Now);

            return nextEpisode;
        }

        public BasicEpisode GetLastAiredEpisode(int id)
        {
            var nextEpisode =
                GetAllSeriesEpisodes(id)
                    .OrderByDescending(x => x.FirstAired)
                    .FirstOrDefault(x => x.FirstAired.HasValue && x.FirstAired.Value <= DateTime.Now);

            return nextEpisode;
        }

    }
}
