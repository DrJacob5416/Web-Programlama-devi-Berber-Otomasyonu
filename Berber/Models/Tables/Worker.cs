using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Berber.Models.Tables
{
    public class Worker
    {
        [Key,ForeignKey("ApplicationUser")]
        public int Id { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public ICollection<WorkerMission> WorkerMissions { get; set; }
    }
}
