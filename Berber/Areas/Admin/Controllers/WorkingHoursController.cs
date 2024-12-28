using Berber.Models.DatabaseOperations.Operations;
using Berber.Models.Tables;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Berber.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class WorkingHoursController : Controller
    {
        private readonly IWorkingHourOp workingHourOp;
        private readonly UserManager<ApplicationUser> userManager;
        public WorkingHoursController(IWorkingHourOp workingHourOp, UserManager<ApplicationUser> userManager)
        {
            this.workingHourOp = workingHourOp;
            this.userManager = userManager;
        }
        [HttpGet]
        public async Task<IActionResult> Index(string userid)
        {
            var hour = workingHourOp.GetByIdWithProps(wh=>wh.WorkerId==userid, "Worker");
            if (hour == null)
            {
                hour = new WorkingHour();
                hour.WorkerId = userid;
                hour.Worker = await userManager.FindByIdAsync(userid);
            }
            return View(hour);
        }
        [HttpPost]
        public async Task<IActionResult> Index(WorkingHour model)
        {
            ModelState.Remove("Worker");
            ViewData["SuccessMessage"] = "İşlem başarısız, modeli kontrol ediniz";
            if (!ModelState.IsValid) return View(model);
            var hour = workingHourOp.GetByIdWithProps(wh => wh.WorkerId == model.WorkerId, "Worker");
            if (hour == null) {
                var newHour = new WorkingHour()
                {
                    WorkerId = model.WorkerId,
                    StartHour = model.StartHour,
                    EndHour = model.EndHour,
                    StartDate = model.StartDate.Date,
                    EndDate = model.EndDate.Date
                };
                workingHourOp.Add(newHour);
                ViewData["SuccessMessage"] = "Ekleme işlemi başarılı";
            }
            else
            {
                hour.EndDate = model.EndDate.Date;
                hour.StartDate = model.StartDate.Date;
                hour.StartHour = model.StartHour;
                hour.EndHour = model.EndHour;
                workingHourOp.Update(hour);
                ViewData["SuccessMessage"] = "Güncelleme işlemi başarılı";
            }
            workingHourOp.Save();
            return View(model);
        }
    }
}
