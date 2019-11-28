using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShowReminder.Web.Models
{
    public class ShowNextLast : Show
    {

        public Episode NextEpisode { get; set; }

        public Episode LastEpisode { get; set; }

    }
}
