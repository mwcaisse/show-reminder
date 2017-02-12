using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShowReminder.API.Models;
using ShowReminder.TVDBFetcher.Model;

namespace ShowReminder.API.Mapper
{
    public static class EpisodeMapper
    {

        public static Episode ToModel(this BasicEpisode episode)
        {
            return new Episode()
            {
                OverallNumber = episode.AbsoluteNumber,
                SeasonNumber = episode.AiredSeason,
                EpisodeNumber = episode.AiredEpisodeNumber,
                AirDate = episode.FirstAired,
                Name = episode.EpisodeName,
                Overview = episode.Overview
            };
        }

        public static IEnumerable<Episode> ToModel(this IEnumerable<BasicEpisode> episodes)
        {
            return episodes.Select(x => x.ToModel());
        }

    }
}
