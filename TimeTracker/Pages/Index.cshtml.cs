using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using TimeTracker.Data;

namespace TimeTracker.Pages
{
    [Authorize]
    [BindProperties]
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;

        public IndexModel(ILogger<IndexModel> logger, ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager)
        {
            _logger = logger;
            this._dbContext = dbContext;
            this._userManager = userManager;
        }
        public WorkWeek Workweek { get; set; }
        public List<Workday> Workdays { get; set; }
        public Worker Worker { get; set; }
        public ApplicationUser CurrentUser { get; set; }
        public bool CheckedIn { get; set; }
        public bool CheckedOut { get; set; }

        public async Task OnGet()
        {
            CurrentUser = await _userManager.GetUserAsync(User);
            Worker = _dbContext.Workers.Find(CurrentUser.WorkerId);
            Workweek = await _dbContext.GetCurrentWorkWeek();
            SetUpWorkdays(Workweek);
            await CheckIfCheckedInOrOut();
        }

        public async Task<IActionResult> OnPostClockIn()
        {
            var workerId = (await _userManager.GetUserAsync(User)).WorkerId;
            var today = await _dbContext.GetCurrentWorkDay(workerId);
            today.ClockIn = DateTime.Now;
            _dbContext.Workdays.Attach(today);
            _dbContext.Entry(today).Property(d => d.ClockIn).IsModified = true;
            _dbContext.SaveChanges();
            return RedirectToPage("/Index");
        }

        public async Task<IActionResult> OnPostClockOut()
        {
            var workerId = (await _userManager.GetUserAsync(User)).WorkerId;
            var today = await _dbContext.GetCurrentWorkDay(workerId);
            today.ClockOut = DateTime.Now;
            _dbContext.Workdays.Attach(today);
            _dbContext.Entry(today).Property(d => d.ClockOut).IsModified = true;
            _dbContext.SaveChanges();
            return RedirectToPage("/Index");
        }

        private void SetUpWorkdays(WorkWeek week)
        {
            if (week.Workdays == null || !week.Workdays.Any(d => d.Worker.Id == Worker.Id))
            {
                var days = new List<Workday>();
                for (var i = 0; i < 7; i++)
                {
                    var workday = new Workday
                    {
                        Worker = Worker,
                        DayOfWeek = (DayOfWeek)i,
                        Workweek = week
                    };
                    days.Add(workday);
                }
                _dbContext.Workdays.AddRange(days);
                _dbContext.SaveChanges();
                Workweek = _dbContext.Workweeks.Find(week.Id);
            }

            Workdays = _dbContext.Workdays.Where(d => d.Workweek.Id == week.Id && d.Worker.Id == Worker.Id).ToList();
        }

        private async Task CheckIfCheckedInOrOut()
        {
            var today = await _dbContext.GetCurrentWorkDay(Worker.Id);
            if (today.ClockIn != null && today.ClockOut == null)
            {
                CheckedIn = true;
            }

            if(today.ClockOut != null)
            {
                CheckedOut = true;
            }
        }


    }
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

    public class WorkWeek
    {
        public WorkWeek()
        {
            Workdays = new HashSet<Workday>();
        }
        public int Id { get; set; }
        public DateTime StartDay { get; set; }
        public DateTime EndDay { get; set; }
        public virtual Worker Worker { get; set; }

        public virtual ICollection<Workday> Workdays { get; set; }
    }
}
