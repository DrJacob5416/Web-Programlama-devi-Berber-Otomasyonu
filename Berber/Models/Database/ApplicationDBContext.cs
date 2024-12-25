﻿using Berber.Models.Tables;
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


            modelBuilder.Entity<WorkingHour>()
                .HasOne(wm => wm.Worker)
                .WithMany()
                .HasForeignKey(wm => wm.WorkerId)
                .OnDelete(DeleteBehavior.Cascade);

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
        DbSet<Mission> Missions { get; set; }
        DbSet<WorkingHour> WorkingHours { get; set; }
    }
}
