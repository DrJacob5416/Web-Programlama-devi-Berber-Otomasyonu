using Microsoft.AspNetCore.Identity;

namespace Berber.Models.Tables
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
        public Worker? Worker { get; set; }
    }
}
