using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeTracker.Models
{
    public class WorkWeek
    {
        public WorkWeek()
        {
            Workdays = new HashSet<Workday>();
        }
        public int Id { get; set; }
        public DateTime StartDay { get; set; }
        public DateTime EndDay { get; set; }

        public virtual ICollection<Workday> Workdays { get; set; }
    }
}
