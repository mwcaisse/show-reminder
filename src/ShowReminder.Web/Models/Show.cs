using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShowReminder.Web.Models
{
    public class Show
    {

        public int Id { get; set; }

        public string Name { get; set; }

        public string Overview { get; set; }

        public string Status { get; set; }

        public string AirsDayOfWeek { get; set; }

        public string AirsTime { get; set; }

        public DateTime? FirstAired { get; set; }

    }
}
