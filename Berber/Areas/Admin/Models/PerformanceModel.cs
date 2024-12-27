namespace Berber.Areas.Admin.Models
{
    public class PerformanceModel
    {
        public string WorkerName { get; set; }
        public int TotalAppointments { get; set; }
        public int Gain { get; set; }
        public int TotalTime { get; set; }
        public double RateByAppointment { get; set; }
        public double RateByTime { get; set; }
        public string Mission { get; set; }
    }
}
