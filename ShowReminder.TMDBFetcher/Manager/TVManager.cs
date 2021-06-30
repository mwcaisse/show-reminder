using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using ShowReminder.TMDBFetcher.Model;
using ShowReminder.TMDBFetcher.Model.Search;
using ShowReminder.TMDBFetcher.Model.TV;

namespace ShowReminder.TMDBFetcher.Manager
{
    public class TVManager : AbstractManager
    {

        private readonly Cache<long, TVShow> _showCache;
        private readonly Cache<Tuple<long, int>, TVSeason> _seasonCache;

        public TVManager(TMDBSettings settings) : base(settings)
        {
            _showCache = new Cache<long, TVShow>();
            _seasonCache = new Cache<Tuple<long, int>, TVSeason>();
        }

        public SearchResult Search(string query, int page = 1)
        {
            var url = $"{BaseUrl}search/tv?language=en-US&query={query}&page={page}&include_adult=true";
            return GetRequest<SearchResult>(url);
        }

        public TVShow GetShow(long id)
        {
            if (_showCache.Contains(id))
            {
                return _showCache.Get(id);
            }

            return FetchShow(id);
        }

        private TVShow FetchShow(long id)
        {
            var url = $"{BaseUrl}tv/{id}";
            var show = GetRequest<TVShow>(url);
            _showCache.Add(id, show);
            return show;
        }

        public TVEpisode GetNextEpisode(long showId)
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

        
        public TVEpisode GetLastEpisode(long showId)
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

        public TVSeason GetSeason(long showId, int seasonNumber)
        {
            if (_seasonCache.Contains(GetSeasonKey(showId, seasonNumber)))
            {
                return _seasonCache.Get(GetSeasonKey(showId, seasonNumber));
            }
            return FetchSeason(showId, seasonNumber);
        }

        private TVSeason FetchSeason(long showId, int seasonNumber)
        {
            var url = $"{BaseUrl}tv/{showId}/season/{seasonNumber}";
            var season = GetRequest<TVSeason>(url);
            _seasonCache.Add(GetSeasonKey(showId, seasonNumber), season);
            return season;
        }

        private Tuple<long, int> GetSeasonKey(long showId, int seasonNumber)
        {
            return new Tuple<long, int>(showId, seasonNumber);
        }

    }
}
