using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting.Internal;
using ShowReminder.Web.Mapper;
using ShowReminder.Web.Models;
using ShowReminder.TMDBFetcher.Manager;

namespace ShowReminder.Web.Manager
{
    public class ShowManager
    {

        private readonly TVManager _tvManager;

        public ShowManager(TVManager tvManager)
        {
            _tvManager = tvManager;
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
            var results = _tvManager.Search(terms);
            return null == results ? new List<Show>() : results.Results.ToModel();
        }

        /// <summary>
        /// Fetches the show with the given Id
        /// </summary>
        /// <param name="id">The id of the show</param>
        /// <returns>The show</returns>
        public Show GetShow(long id)
        {
            var result = _tvManager.GetShow(id);
            return result?.ToModel();
        }

        /// <summary>
        /// Fetches the show with the given Id, and the Next/Last episodes populated
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ShowNextLast GetShowWithNextLast(long id)
        {
            var show = GetShow(id);
            if (null != show)
            {
                var nextLastShow = new ShowNextLast().PopulateFromShow(show);
                nextLastShow.NextEpisode = _tvManager.GetNextEpisode(id)?.ToModel();
                nextLastShow.LastEpisode = _tvManager.GetLastEpisode(id)?.ToModel();
                return nextLastShow;
            }
            return null;
        }
    }
}
