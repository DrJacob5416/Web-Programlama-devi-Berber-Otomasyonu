using Berber.Areas.Admin.Models;
using Berber.Models.DatabaseOperations.Operations;
using Berber.Models.Tables;
using Microsoft.AspNetCore.Mvc;

namespace Berber.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class WorkerMissionsController : Controller
    {
        private readonly IWorkerMissionOp workerMissionOp;

        public WorkerMissionsController(IWorkerMissionOp workerMissionOp)
        {
            this.workerMissionOp = workerMissionOp;
        }

        public IActionResult Index(string userid)
        {
            ViewBag.Userid = userid;
            return View(workerMissionOp.GetAllByCondition(wm=>wm.WorkerId==userid,("Mission")));
        }
        public IActionResult ChangeStatus(int id, string userId)
        {
            var workerMission = workerMissionOp.GetById(id);
            if (workerMission.isSpecialist)
            {
                workerMission.isSpecialist = false;
            }
            else
            {
                workerMission.isSpecialist = true;
            }
            workerMissionOp.Update(workerMission);
            workerMissionOp.Save();
            return RedirectToAction("Index", "WorkerMissions", new {userid=userId});
        }
        public IActionResult Delete(int id, string userId)
        {
            workerMissionOp.Delete(workerMissionOp.GetById(id));
            workerMissionOp.Save();
            return RedirectToAction("Index", "WorkerMissions", new { userid = userId });
        }
        [HttpPost]
        public IActionResult Create(CreateWorkerMissionModel model)
        {
            workerMissionOp.Add(new WorkerMission() { MissionId=model.MissionId, WorkerId=model.WorkerId, Time= model.Time, Price=model.Price});
            workerMissionOp.Save();
            return RedirectToAction("Index", "WorkerMissions", new { userid = model.WorkerId });
        }
    }
}
