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
        [Display(Name = "Hizmet")]
        public int MissionId { get; set; }
        [ForeignKey(nameof(MissionId))]
        public Mission Mission { get; set; }
        [Display(Name = "İşlem Ücreti (TL)")]
        public int Price { get; set; }
        [Display(Name = "İşlem Süresi (dk)")]
        public int Time { get; set; }
        public bool isSpecialist { get; set; }
        public ICollection<Appointment> Appointments { get; set; }
    }
}
