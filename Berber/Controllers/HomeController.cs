using System.Diagnostics;
using Berber.Models;
using Berber.Models.DatabaseOperations.Operations;
using Berber.Models.ServiceModels;
using Berber.Models.Tables;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Berber.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAppointmentOp _appointmentOp;
        private readonly IMissionOp _missionOp;
        private readonly IWorkerMissionOp _workerMissionOp;
        private readonly IWorkingHourOp _workingHourOp;
        private readonly UserManager<ApplicationUser> _userManager;

        public HomeController(ILogger<HomeController> logger, IAppointmentOp appointmentOp, IMissionOp missionOp, IWorkerMissionOp workerMissionOp, IWorkingHourOp workingHourOp, UserManager<ApplicationUser> userManager)
        {
            _appointmentOp = appointmentOp;
            _missionOp = missionOp;
            _workerMissionOp = workerMissionOp;
            _workingHourOp = workingHourOp;
            _userManager = userManager;
        }

        public IActionResult Index(int? missionid)
        {
            var missions = _missionOp.GetAll("WorkerMissions,WorkerMissions.Worker");
            ViewBag.MissionId = missionid;
            return View(missions);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAppointment([FromBody] RandevuModel model)
        {
            if (model == null || string.IsNullOrEmpty(model.DayTime) || string.IsNullOrEmpty(model.HourTime) || string.IsNullOrEmpty(model.WorkerMissionId))
            {
                return Json(new { success = false, message = "Geçersiz veri gönderildi." });
            }
            var dayDate = DateTime.Parse(model.DayTime);
            var hourTime = TimeSpan.Parse(model.HourTime);
            var workerMissionId = int.Parse(model.WorkerMissionId);
            var workerMission = _workerMissionOp.GetByIdWithProps(wm=>wm.Id==workerMissionId, "Worker,Worker.WorkingHour,Mission");
            var workerMissionTime = workerMission.Time;
            var finishTime = hourTime.Add(TimeSpan.FromMinutes(workerMissionTime));
            var workingHour = _workingHourOp.GetByIdWithProps(wh => wh.WorkerId == workerMission.WorkerId);
            var workerStartDate = workingHour.StartDate;
            var workerEndDate = workingHour.EndDate;
            var workerStartHour = workingHour.StartHour;
            var workerEndHour = workingHour.EndHour;
            if(workerStartDate>dayDate || workerEndDate<dayDate)
                return Json(new { success = false, message = $"Baþarýsýz. Çalýþan için randevu tarih aralýðý {workerStartDate} - {workerEndDate}" });
            if (workerStartHour > hourTime || finishTime > workerEndHour)
                return Json(new { success = false, message = $"Baþarýsýz. Çalýþan için randevu saat aralýðý {workerStartHour} - {workerEndHour}" });
            var workerAppointments = _appointmentOp.GetAllByCondition(wa => wa.WorkerMission.WorkerId == workerMission.WorkerId && wa.DayTime == dayDate,"WorkerMission");
            foreach( var workerAppointment in workerAppointments)
            {
                var start = workerAppointment.HourTime;
                var finish = workerAppointment.HourTime.Add(TimeSpan.FromMinutes(workerAppointment.WorkerMission.Time));
                if ((start<=hourTime && hourTime<=finish) || (start <= finishTime && finishTime <= finish))
                    return Json(new { success = false, message = $"Baþarýsýz. Çalýþanýn randevusu var. Ýlgili randevu : {start} - {finish}" });
            }
            var currentUser = await _userManager.FindByEmailAsync(User.Identity.Name);
            var userAppointments = _appointmentOp.GetAllByCondition(wa => wa.UserId == currentUser.Id && wa.DayTime == dayDate, "WorkerMission");
            foreach (var userAppointment in userAppointments)
            {
                var start = userAppointment.HourTime;
                var finish = userAppointment.HourTime.Add(TimeSpan.FromMinutes(userAppointment.WorkerMission.Time));
                if ((start <= hourTime && hourTime <= finish) || (start <= finishTime && finishTime <= finish))
                    return Json(new { success = false, message = $"Baþarýsýz. Zaten bir randevunuz var. Ýlgili randevu : {start} - {finish}" });
            }
            var appointment = new Appointment()
            {
                UserId = currentUser.Id,
                DayTime = dayDate,
                HourTime = hourTime,
                WorkerMissionId = workerMissionId,
            };
            _appointmentOp.Add(appointment);
            _appointmentOp.Save();
            return Json(new { success = true, message = "Randevu baþarýyla alýndý." });
        }
        public async Task<IActionResult> AppointmentHistory()
        {
            var currentUser = await _userManager.FindByEmailAsync(User.Identity.Name);
            var appointments = _appointmentOp.GetAllByCondition(x => x.UserId == currentUser.Id, "WorkerMission,WorkerMission.Worker,WorkerMission.Mission");

            return View(appointments);
        }
        public IActionResult CancelAppointment(int id)
        {
            return RedirectToAction("AppointmentHistory","Home");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
