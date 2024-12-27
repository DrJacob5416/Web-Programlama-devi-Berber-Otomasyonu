using System.ComponentModel.DataAnnotations.Schema;

namespace Berber.Models.Tables
{
    public class Appointment
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public ApplicationUser User { get; set; }
        public DateTime DayTime { get; set; }
        public TimeSpan HourTime { get; set; }
        public int WorkerMissionId { get; set; }
        [ForeignKey(nameof(WorkerMissionId))]
        public WorkerMission WorkerMission { get; set; }
        public bool? IsApproval { get; set; }
    }
}
