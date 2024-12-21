using System.ComponentModel.DataAnnotations;

namespace Berber.Models.Tables
{
    public class Mission
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<WorkerMission> WorkerMissions { get; set; }
    }
}
