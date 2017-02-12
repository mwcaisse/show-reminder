using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShowReminder.API.Mapper;
using ShowReminder.API.Models;
using ShowReminder.TVDBFetcher.Manager;
using ShowReminder.TVDBFetcher.Model.Authentication;

namespace ShowReminder.API.Manager
{
    public class ShowManager
    {

        private readonly SearchManager _searchManager;

        private readonly SeriesManager _seriesManager;

        public ShowManager(AuthenticationParam authParam)
        {
            _searchManager = new SearchManager(authParam);
            _seriesManager = new SeriesManager(authParam);
        }

        /// <summary>
        ///  Searches for shows by the given terms.
        /// 
        ///  Looks for shows that Name and/or Alias contain/match the given terms
        /// </summary>
        /// <param name="terms"></param>
        /// <returns></returns>
        public IEnumerable<Show> Search(string terms)
        {
            var results = _searchManager.SearchByName(terms);
            return results.Data.ToModel();
        }

        /// <summary>
        /// Fetches the show with the given Id
        /// </summary>
        /// <param name="id">The id of the show</param>
        /// <returns>The show</returns>
        public Show GetShow(int id)
        {
            var result = _seriesManager.GetSeries(id);
            return result?.ToModel();
        }

        /// <summary>
        /// Fetches the show with the given Id, and the Next/Last episodes populated
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ShowNextLast GetShowWithNextLast(int id)
        {
            var show = GetShow(id);
            if (null != show)
            {
                var nextLastShow = new ShowNextLast().PopulateFromShow(show);
                var showEpisodes = _seriesManager.GetAllSeriesEpisodes(id)?.ToModel().ToList();
                if (null != showEpisodes)
                {
                    nextLastShow.NextEpisode = GetNextAiringEpisode(showEpisodes);
                    nextLastShow.LastEpisode = GetLastAiredEpisode(showEpisodes);
                }
                return nextLastShow;
            }
            return null;
        }

        protected Episode GetNextAiringEpisode(IEnumerable<Episode> episodes)
        {
            return
                episodes.OrderBy(x => x.AirDate)
                    .FirstOrDefault(x => x.AirDate.HasValue && x.AirDate.Value > DateTime.Now);
        }

        protected Episode GetLastAiredEpisode(IEnumerable<Episode> episodes)
        {
            return
                episodes.OrderByDescending(x => x.AirDate)
                    .FirstOrDefault(x => x.AirDate.HasValue && x.AirDate.Value <= DateTime.Now);
        }

    }
}
