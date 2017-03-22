using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using ShowReminder.TMDBFetcher.Model;
using ShowReminder.TMDBFetcher.Model.Search;
using ShowReminder.TMDBFetcher.Model.TV;

namespace ShowReminder.TMDBFetcher.Manager
{
    public class ShowManager : AbstractManager
    {

        public ShowManager(TMDBSettings settings) : base(settings)
        {
            
        }

        public SearchResult Search(string query, int page = 1)
        {
            var url = $"{BaseUrl}search/tv?language=en-US&query={query}&page={page}&include_adult=true";
            return GetRequest<SearchResult>(url);
        }

        public TVShow GetShow(int id)
        {
            var url = $"{BaseUrl}tv/{id}";
            return GetRequest<TVShow>(url);
        }

        public TVEpisode GetNextEpisode(int showId)
        {
            TVShow show = GetShow(showId);

            TVSeason latestSeason = show.Seasons
                .Where(x => x.AirDate <= DateTime.Now)
                .OrderByDescending(x => x.AirDate)
                .FirstOrDefault();

            latestSeason = GetSeason(showId, latestSeason.SeasonNumber);

            TVEpisode nextEpisode =
                latestSeason.Episodes
                    .Where(x => x.AirDate > DateTime.Now)
                    .OrderBy(x => x.AirDate)
                    .FirstOrDefault();

            return nextEpisode;
        }

        
        public TVEpisode GetLastEpisode(int showId)
        {

            //TODO: Account for the case where the lastEpisode is in the previous season
            TVShow show = GetShow(showId);

            TVSeason latestSeason = show.Seasons
                .Where(x => x.AirDate <= DateTime.Now)
                .OrderByDescending(x => x.AirDate)
                .FirstOrDefault();

            latestSeason = GetSeason(showId, latestSeason.SeasonNumber);

            TVEpisode lastEpisode =
                latestSeason.Episodes
                    .Where(x => x.AirDate <= DateTime.Now)
                    .OrderByDescending(x => x.AirDate)
                    .FirstOrDefault();

            return lastEpisode;
        }

        public TVSeason GetSeason(int showId, int seasonNumber)
        {
            var url = $"{BaseUrl}tv/{showId}/season/{seasonNumber}";
            return GetRequest<TVSeason>(url);
        }

    }
}
