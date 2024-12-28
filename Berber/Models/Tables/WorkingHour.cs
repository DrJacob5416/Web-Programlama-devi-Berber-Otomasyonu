using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Berber.Models.Tables
{
    public class WorkingHour
    {
        public int Id { get; set; }
        public string WorkerId { get; set; }
        [ForeignKey(nameof(WorkerId))]
        public ApplicationUser Worker { get; set; }
        [Display(Name = "Mesai Başlama")]
        public TimeSpan StartHour { get; set; }
        [Display(Name = "Mesai Bitiş")]
        public TimeSpan EndHour { get; set; }
        [Display(Name = "Geçerlilik Tarihi Başlangıç")]
        public DateTime StartDate { get; set; }
        [Display(Name = "Geçerlilik Tarihi Bitiş")]
        public DateTime EndDate { get; set; }
    }
}
