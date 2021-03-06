﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShowReminder.Web.Models
{
    public class Episode
    {

        public int? OverallNumber { get; set; }

        public int? SeasonNumber { get; set; }

        public int? EpisodeNumber { get; set; }

        public DateTime? AirDate { get; set; }

        public string Name { get; set; }

        public string Overview { get; set; }

    }
}
