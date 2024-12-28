using Berber.Models.DatabaseOperations.Operations;
using Berber.Models.Tables;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Berber.Controllers
{
    [Authorize(Roles ="Worker")]
    public class WorkersController : Controller
    {
        private readonly IAppointmentOp appointmentOp;
        private readonly UserManager<ApplicationUser> userManager;

        public WorkersController(IAppointmentOp appointmentOp, UserManager<ApplicationUser> userManager)
        {
            this.appointmentOp = appointmentOp;
            this.userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var user = await userManager.FindByEmailAsync(User.Identity.Name);
            var appoinments = appointmentOp.GetAllByCondition(x => x.WorkerMission.WorkerId == user.Id, "User,WorkerMission,WorkerMission.Mission");

            return View(appoinments.OrderByDescending(m=>m.DayTime).ToList());
        }
    }
}
