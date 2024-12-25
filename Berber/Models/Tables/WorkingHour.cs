using System.ComponentModel.DataAnnotations.Schema;

namespace Berber.Models.Tables
{
    public class WorkingHour
    {
        public int Id { get; set; }
        public string WorkerId { get; set; }
        [ForeignKey(nameof(WorkerId))]
        public ApplicationUser Worker { get; set; }
        public TimeSpan StartHour { get; set; }
        public TimeSpan EndHour { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
