using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeTracker.Data
{
    public class ApplicationUser : IdentityUser
    {
        public int WorkerId { get; set; }
    }
}
