using Berber.Areas.Admin.Models;
using Berber.Models.DatabaseOperations.Operations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Berber.Areas.Admin.ViewComponents
{
    public class CreateWorkerMission : ViewComponent
    {
        private readonly IMissionOp missionOp;

        public CreateWorkerMission(IMissionOp missionOp)
        {
            this.missionOp = missionOp;
        }
        public IViewComponentResult Invoke(string workerid)
        {
            var missions = missionOp.GetAll();
            var list = missions.Select(m => new SelectListItem
            {
                Text = m.Name,
                Value = m.Id.ToString()
            }).ToList();
            return View(new CreateWorkerMissionModel() {WorkerId= workerid, Missions= new SelectList(list, "Value", "Text") });
        }
    }
}
