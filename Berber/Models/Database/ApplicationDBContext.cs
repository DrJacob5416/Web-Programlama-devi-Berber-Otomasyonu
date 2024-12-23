using Berber.Models.Tables;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace Berber.Models.Database
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Worker>()
                .HasOne(w => w.ApplicationUser)
                .WithOne(u => u.Worker)
                .HasForeignKey<Worker>(w => w.Id);

           
            modelBuilder.Entity<WorkerMission>()
                .HasOne(wm => wm.Worker)         
                .WithMany(w => w.WorkerMissions)  
                .HasForeignKey(wm => wm.WorkerId)
                .OnDelete(DeleteBehavior.Cascade); 

            
            modelBuilder.Entity<WorkerMission>()
                .HasOne(wm => wm.Mission)         
                .WithMany(m => m.WorkerMissions)                      
                .HasForeignKey(wm => wm.MissionId) 
                .OnDelete(DeleteBehavior.Restrict);
        }
        DbSet<WorkerMission> WorkMissions { get; set; }
        DbSet<Worker> Workers { get; set; }
        DbSet<Mission> Missions { get; set; }
    }
}
