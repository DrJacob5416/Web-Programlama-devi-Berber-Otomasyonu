using Microsoft.AspNetCore.Identity;

namespace Berber.Models.Tables
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
        public WorkingHour? WorkingHour { get; set; }
        public ICollection<WorkerMission> WorkerMissions { get; set; }
    }
}
