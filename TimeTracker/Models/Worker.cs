using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeTracker.Models
{
    public class Worker
    {
        public Worker()
        {
            Workdays = new HashSet<Workday>();
        }
        public int Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get { return $"{FirstName} {LastName}"; } }
        public decimal HourlyPay { get; set; }
        public virtual ICollection<Workday> Workdays { get; set; }
    }
}
