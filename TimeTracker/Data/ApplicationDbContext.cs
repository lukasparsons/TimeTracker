using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TimeTracker.Pages;
using static TimeTracker.Pages.IndexModel;

namespace TimeTracker.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IHttpContextAccessor httpContextAccessor)
            : base(options)
        {
            this.httpContextAccessor = httpContextAccessor;
        }
        private readonly IHttpContextAccessor httpContextAccessor;
        public DbSet<Worker> Workers { get; set; }
        public DbSet<Workday> Workdays { get; set; }
        public DbSet<WorkWeek> Workweeks { get; set; }

        public async Task<WorkWeek> GetCurrentWorkWeek()
        {
            var today = DateTime.Today;
            var currentWorkWeek = await Workweeks.FirstOrDefaultAsync(ww => ww.StartDay <= today && ww.EndDay >= today);

            if (currentWorkWeek == null)
            {
                currentWorkWeek = await CreateCurrentWorkWeek();
            }

            return currentWorkWeek;
        }
        public async Task<Workday> GetCurrentWorkDay(int workerId)
        {
            var week = await GetCurrentWorkWeek();
            return week.Workdays.FirstOrDefault(d => d.IsCurrentDayOfWeek && d.Worker.Id == workerId);
        }

        private async Task<WorkWeek> CreateCurrentWorkWeek()
        {
            var today = DateTime.Today;
            var sunday = today.AddDays(-(int)today.DayOfWeek);
            var saturday = sunday.AddDays((int)DayOfWeek.Saturday);
            var user = Users.Find(GetCurrentUserId());

            var workWeek = new WorkWeek
            {
                StartDay = sunday,
                EndDay = saturday,
                Worker = Workers.Find(user.WorkerId)
            };

            this.Workweeks.Add(workWeek);
            await this.SaveChangesAsync();

            return workWeek;
        }
        private string GetCurrentUserId()
        {
            var userId = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return userId;
        }


    }
}
