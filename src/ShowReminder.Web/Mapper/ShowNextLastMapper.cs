using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShowReminder.Web.Models;

namespace ShowReminder.Web.Mapper
{
    public static class ShowNextLastMapper
    {

        public static ShowNextLast PopulateFromShow(this ShowNextLast nextLast, Show show)
        {
            nextLast.Id = show.Id;
            nextLast.Name = show.Name;
            nextLast.Overview = show.Overview;
            nextLast.Status = show.Status;
            nextLast.AirsDayOfWeek = show.AirsDayOfWeek;
            nextLast.AirsTime = show.AirsTime;
            nextLast.FirstAired = show.FirstAired;
            return nextLast;
        }

    }
}
