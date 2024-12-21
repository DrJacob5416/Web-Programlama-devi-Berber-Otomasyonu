using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Berber.Models.Tables
{
    public class WorkerMission
    {
        public int WorkerId { get; set; }
        [ForeignKey(nameof(WorkerId))]
        public Worker Worker { get; set; }
        public int MissionId { get; set; }
        [ForeignKey(nameof(MissionId))]
        public Mission Mission { get; set; }
        public int Price { get; set; }
        public int Time { get; set; }
        public bool isSpecialist { get; set; }
    }
}
