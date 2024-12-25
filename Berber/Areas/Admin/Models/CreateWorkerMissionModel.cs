using Microsoft.AspNetCore.Mvc.Rendering;

namespace Berber.Areas.Admin.Models
{
    public class CreateWorkerMissionModel
    {
        public int MissionId { get; set; }
        public int Price { get; set; }
        public int Time { get; set; }
        public string WorkerId { get; set; }
        public SelectList? Missions { get; set; }
    }
}
