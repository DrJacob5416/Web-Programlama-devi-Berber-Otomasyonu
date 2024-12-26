using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Berber.Models.Tables
{
    public class WorkerMission
    {
        [Key]
        public int Id { get; set; }
        public string WorkerId { get; set; }
        [ForeignKey(nameof(WorkerId))]
        public ApplicationUser Worker { get; set; }
        public int MissionId { get; set; }
        [ForeignKey(nameof(MissionId))]
        public Mission Mission { get; set; }
        public int Price { get; set; }
        public int Time { get; set; }
        public bool isSpecialist { get; set; }
        public ICollection<Appointment> Appointments { get; set; }
    }
}
