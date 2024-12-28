using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Berber.Areas.Admin.Models
{
    public class CreateWorkerMissionModel
    {
        [Display(Name = "İşlem")]
        public int MissionId { get; set; }
        [Display(Name = "İşlem Ücreti (Tl)")]
        public int Price { get; set; }
        [Display(Name = "İşlem Süresi (dk)")]
        public int Time { get; set; }
        public string WorkerId { get; set; }
        public SelectList? Missions { get; set; }
    }
}
