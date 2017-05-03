using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShowReminder.API.Models;
using ShowReminder.Data;
using ShowReminder.Data.Entity;

namespace ShowReminder.API.Manager
{
    public class TrackedShowManager
    {

        private readonly ShowManager _showManager;

        private readonly DataContext _dataContext;

        public TrackedShowManager(DataContext dataContext, ShowManager showManager)
        {
            _dataContext = dataContext;
            _showManager = showManager;
        }

        /// <summary>
        /// Fetch a Tracked show by its Id
        /// </summary>
        /// <param name="id">The id of the show to fetch</param>
        /// <returns>The show, or null if no show was found</returns>
        public TrackedShow Get(long id)
        {
            return _dataContext.Shows
                .Include(x => x.NextEpisode)
                .Include(x => x.LastEpisode)
                .FirstOrDefault(x => x.Id == id);
        }

        /// <summary>
        /// Fetches all of the tracked shows
        /// </summary>
        /// <returns></returns>
        public List<TrackedShow> GetAll()
        {
            var shows = _dataContext.Shows
                .Include(x => x.NextEpisode)
                .Include(x => x.LastEpisode)
                .ToList();

            RefetchExpiredShows(shows);

            return shows.OrderBy(x =>
                null == x.NextEpisode ? DateTime.MaxValue : x.NextEpisode.AirDate)
                .ThenBy(x => x.Name)
                .ToList();
        }

        protected void RefetchExpiredShows(IEnumerable<TrackedShow> shows)
        {
            foreach (TrackedShow show in shows)
            {
                if ((null == show.NextEpisode || show.NextEpisode.AirDate < DateTime.Now.Date) 
                    && show.UpdateDate.Date != DateTime.Now.Date)
                {
                    RefetchShow(show);
                }
            }
        }

        /// <summary>
        /// Updates the given show with updated information from the source
        /// </summary>
        /// <param name="show"></param>
        protected void RefetchShow(TrackedShow show)
        {
            var sourceShow = _showManager.GetShowWithNextLast(show.TvdbId);
            UpdatedTrackedShowFromShow(show, sourceShow);

            _dataContext.SaveChanges();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="showId"></param>
        /// <returns></returns>
        public TrackedShow Add(int showId)
        {
            var exists = _dataContext.Shows.Any(x => x.TvdbId == showId);
            if (exists)
            {
                throw new ManagerException("This show has already been added! Cannot be re-added");
            }

            var show = _showManager.GetShowWithNextLast(showId);
            if (null == show)
            {
                throw new ManagerException("No show with the given id exists!");
            }

            TrackedShow trackedShow = CreatedTrackedShowFromShow(show);

            _dataContext.Shows.Add(trackedShow);
            _dataContext.SaveChanges();

            return trackedShow;
        }

        protected TrackedEpisode CreateTrackedEpisodeFromEpisode(Episode episode)
        {
            if (null == episode)
            {
                return null;
            }
            return new TrackedEpisode()
            {
                OverallNumber = episode.OverallNumber,
                AirDate = episode.AirDate,
                Name = episode.Name,
                EpisodeNumber = episode.EpisodeNumber,
                SeasonNumber = episode.SeasonNumber,
                Overview = episode.Overview
            };
        }

        protected TrackedShow CreatedTrackedShowFromShow(ShowNextLast show)
        {
            var trackedShow = new TrackedShow()
            {
                TvdbId = show.Id,
                Name = show.Name,
                FirstAiredDate = show.FirstAired,
                AirDay = show.AirsDayOfWeek,
                AirTime = show.AirsTime
            };

            trackedShow.LastEpisode = CreateTrackedEpisodeFromEpisode(show.LastEpisode);
            trackedShow.NextEpisode = CreateTrackedEpisodeFromEpisode(show.NextEpisode);
           

            return trackedShow;
        }

        protected TrackedShow UpdatedTrackedShowFromShow(TrackedShow trackedShow, ShowNextLast show)
        {
            trackedShow.Name = show.Name;
            trackedShow.FirstAiredDate = show.FirstAired;
            trackedShow.AirDay = show.AirsDayOfWeek;
            trackedShow.AirTime = show.AirsTime;

            trackedShow.LastEpisode = CreateTrackedEpisodeFromEpisode(show.LastEpisode);
            trackedShow.NextEpisode = CreateTrackedEpisodeFromEpisode(show.NextEpisode);

            return trackedShow;
        }


    }
}
