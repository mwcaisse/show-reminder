using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShowReminder.Web.Models;
using ShowReminder.TMDBFetcher.Model.TV;

namespace ShowReminder.Web.Mapper
{
    public static class ShowMapper
    {
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
            }

            return model;
        }

        public static IEnumerable<Show> ToModel(this IEnumerable<TVShow> shows)
        {
            return shows.Select(x => x.ToModel());
        }
    }
}
