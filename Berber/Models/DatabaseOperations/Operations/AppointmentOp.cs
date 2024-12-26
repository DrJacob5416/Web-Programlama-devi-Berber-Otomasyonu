using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System;
using Berber.Models.Tables;
using Berber.Models.Database;

namespace Berber.Models.DatabaseOperations.Operations
{
    public class AppointmentOp : Generic<Appointment>, IAppointmentOp
    {
        private readonly ApplicationDbContext _appDbContext;
        private readonly DbSet<Appointment> appointmentbl;
        public AppointmentOp(ApplicationDbContext appDbContext) : base(appDbContext)
        {
            _appDbContext = appDbContext;
            appointmentbl = _appDbContext.Set<Appointment>();
        }
    }
}
