using System.Collections.Generic;
using System.Linq;
using ShowReminder.TMDBFetcher.Model.TV;
using ShowReminder.Web.Models;


namespace ShowReminder.Web.Mapper
{
    public static class EpisodeMapper
    {
        public static Episode ToModel(this TVEpisode episode)
        {
            return new Episode()
            {
                SeasonNumber = episode.SeasonNumber,
                EpisodeNumber = episode.EpisodeNumber,
                AirDate = episode.AirDate,
                Name = episode.Name,
                Overview = episode.Overview
            };
        }

        public static IEnumerable<Episode> ToModel(this IEnumerable<TVEpisode> episodes)
        {
            return episodes.Select(x => x.ToModel());
        }

    }
}
