using Berber.Areas.Admin.Models;
using Berber.Models.DatabaseOperations.Operations;
using Berber.Models.Tables;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Berber.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ManagementController : Controller
    {
        private readonly IAppointmentOp _appointmentOp;
        private readonly IMissionOp _missionOp;
        private readonly IWorkerMissionOp _workerMissionOp;
        private readonly IWorkingHourOp _workingHourOp;
        private readonly UserManager<ApplicationUser> _userManager;

        public ManagementController(IAppointmentOp appointmentOp, IMissionOp missionOp, IWorkerMissionOp workerMissionOp, IWorkingHourOp workingHourOp, UserManager<ApplicationUser> userManager)
        {
            _appointmentOp = appointmentOp;
            _missionOp = missionOp;
            _workerMissionOp = workerMissionOp;
            _workingHourOp = workingHourOp;
            _userManager = userManager;
        }

        public IActionResult Index(string status ="Tümü")
        {
            var allAppointments = new List<Appointment>();
            if (status == "Tümü")
            {
                allAppointments = _appointmentOp.GetAll("User,WorkerMission,WorkerMission.Worker,WorkerMission.Mission").ToList();
            }else if(status == "Onaylandi") {
                allAppointments = _appointmentOp.GetAllByCondition(a=>a.IsApproval==true,"User,WorkerMission,WorkerMission.Worker,WorkerMission.Mission").ToList();
            }
            else if(status == "Beklemede")
            {
                allAppointments = _appointmentOp.GetAllByCondition(a => a.IsApproval == null, "User,WorkerMission,WorkerMission.Worker,WorkerMission.Mission").ToList();
            }
            else if (status == "IptalEdildi")
            {
                allAppointments = _appointmentOp.GetAllByCondition(a => a.IsApproval == false, "User,WorkerMission,WorkerMission.Worker,WorkerMission.Mission").ToList();
            }

            return View(allAppointments);
        }
        public IActionResult CancelAppointment(int id)
        {
            var appointment = _appointmentOp.GetById(id);
            appointment.IsApproval = false;
            _appointmentOp.Update(appointment);
            _appointmentOp.Save();
            TempData["Appointment"] = $"{appointment.DayTime.ToShortDateString()} tarihli randevu iptal edildi";
            return RedirectToAction("Index", "Management", new {area="Admin"});
        }
        public IActionResult ApproveAppointment(int id)
        {
            var appointment = _appointmentOp.GetById(id);
            appointment.IsApproval = true;
            _appointmentOp.Update(appointment);
            _appointmentOp.Save();
            TempData["Appointment"] = $"{appointment.DayTime.ToShortDateString()} tarihli randevu onaylandı";
            return RedirectToAction("Index", "Management", new { area = "Admin" });
        }
        public async Task<IActionResult> Performance()
        {
            
            var workers = await _userManager.GetUsersInRoleAsync("Worker");
            var performanceList = new List<PerformanceModel>();
            foreach (var worker in workers)
            {
                var missions = new List<string>();
                int gain = 0;
                int totalAppointments = 0;
                int time = 0;
                var allAppoitments = _appointmentOp.GetAllByCondition(a => a.WorkerMission.WorkerId==worker.Id && a.IsApproval == true, "User,WorkerMission,WorkerMission.Worker,WorkerMission.Mission");
                if(allAppoitments!=null && allAppoitments.Any())
                {
                    foreach (var appointment in allAppoitments)
                    {
                        time += appointment.WorkerMission.Time;
                        gain += appointment.WorkerMission.Price;
                        totalAppointments++;
                        missions.Add(appointment.WorkerMission.Mission.Name);
                    }
                }

                string mis = "";
                if (missions != null && missions.Any())
                {
                    var mostFrequent = missions
                                .GroupBy(m => m)
                                .OrderByDescending(g => g.Count())
                                .FirstOrDefault();
                    mis = mostFrequent.Key;
                }
                    int rateByAppointment = 0;
                int rateByTime = 0;
                if (totalAppointments != 0)
                    rateByAppointment = gain / totalAppointments;
                if (time != 0)
                    rateByTime = gain / time;
                if (time != 0)
                    rateByTime = gain / time;
                var model = new PerformanceModel()
                {
                    WorkerName = worker.FullName,
                    TotalAppointments = totalAppointments,
                    Gain = gain,
                    RateByAppointment = rateByAppointment,
                    Mission = mis,
                    TotalTime = time,
                    RateByTime = rateByTime
                };
                performanceList.Add(model);
            }
            return View(performanceList);
        }
    }
}
