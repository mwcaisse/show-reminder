using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShowReminder.Data.Entity
{
    public class BaseEntity
    {

        public long Id { get; set; }

        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }

        public BaseEntity()
        {
            CreateDate = DateTime.Now;
            UpdateDate = DateTime.Now;
        }


    }
}
