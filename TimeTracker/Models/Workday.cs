using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeTracker.Models
{
    public class Workday
    {
        public int Id { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public DateTime? ClockIn { get; set; }
        public DateTime? ClockOut { get; set; }
        public bool IsCurrentDayOfWeek
        {
            get
            {
                var isCurrentDayOfWeek = this.DayOfWeek == DateTime.Now.DayOfWeek;
                return isCurrentDayOfWeek;
            }
        }
        public TimeSpan HoursWorked
        {
            get
            {
                return ClockOut - ClockIn ?? new TimeSpan();
            }
        }
        public virtual Worker Worker { get; set; }
        public virtual WorkWeek Workweek { get; set; }
    }
}
