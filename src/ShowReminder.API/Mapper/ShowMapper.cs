using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShowReminder.TVDBFetcher.Model;
using ShowReminder.API.Models;
using ShowReminder.TMDBFetcher.Model.TV;
using ShowReminder.TVDBFetcher.Model.Search;
using ShowReminder.TVDBFetcher.Model.Series;

namespace ShowReminder.API.Mapper
{
    public static class ShowMapper
    {

        public static Show ToModel(this Series show)
        {
            return new Show()
            {
                Id = show.Id,
                Name = show.SeriesName,
                Overview = show.Overview,
                Status = show.Status,
                AirsDayOfWeek = show.AirsDayOfWeek,
                AirsTime = show.AirsTime,
                FirstAired = show.FirstAired
            };
        }

        public static IEnumerable<Show> ToModel(this IEnumerable<Series> shows)
        {
            return shows.Select(x => x.ToModel());
        }

        public static Show ToModel(this SearchSeries show)
        {
            return new Show()
            {
                Id = show.Id,
                Name = show.SeriesName,
                Overview = show.Overview,
                Status = show.Status,
                FirstAired = show.FirstAired
            };
        }

        public static IEnumerable<Show> ToModel(this IEnumerable<SearchSeries> shows)
        {
            return shows.Select(x => x.ToModel());
        }

        public static Show ToModel(this TVShow show)
        {
            var model = new Show()
            {
                Id = show.Id,
                Name = show.Name,
                Overview = show.Overview,
                Status = show.Status,
                FirstAired = show.FirstAirDate
            };

            var airDate = show.LastAirDate ?? show.FirstAirDate;

            if (null != airDate)
            {
                model.AirsDayOfWeek = airDate.Value.ToString("dddd");
                model.AirsTime = airDate.Value.ToString("HH:mm");
            }

            return model;
        }

        public static IEnumerable<Show> ToModel(this IEnumerable<TVShow> shows)
        {
            return shows.Select(x => x.ToModel());
        }
    }
}
