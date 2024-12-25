using Berber.Models.DatabaseOperations.Operations;
using Berber.Models.ServiceModels;
using Berber.Models.Tables;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Security.Claims;

namespace Berber.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MissionsController : Controller
    {
        private readonly IMissionOp missionOp;
        private readonly IWorkerMissionOp workerMissionOp;

        public MissionsController(IMissionOp missionOp, IWorkerMissionOp workerMissionOp)
        {
            this.missionOp = missionOp;
            this.workerMissionOp = workerMissionOp;
        }

        public IActionResult Index()
        {
            return View(missionOp.GetAll());
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(MissionCreateModel model)
        {
            if(ModelState.IsValid)
            {
                missionOp.Add(new Mission() { Name=model.Name});
                missionOp.Save();
                return RedirectToAction("Index","Missions");
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult Update(int id)
        {
            return View(missionOp.GetById(id));
        }
        [HttpPost]
        public IActionResult Update(Mission mission)
        {
            ModelState.Remove("WorkerMissions");
            if (ModelState.IsValid)
            {
                var mis = missionOp.GetById(mission.Id);
                mis.Name = mission.Name;
                missionOp.Update(mis);
                missionOp.Save();
                return RedirectToAction("Index", "Missions");
            }
            return View(mission);
        }
        public IActionResult Delete(int id)
        {
            missionOp.Delete(missionOp.GetById(id));
            missionOp.Save();
            return RedirectToAction("Index", "Missions");
        }
    }
}
