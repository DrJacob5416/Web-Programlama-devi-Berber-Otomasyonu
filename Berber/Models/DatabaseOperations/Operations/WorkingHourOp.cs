using Berber.Models.Database;
using Berber.Models.Tables;
using Microsoft.EntityFrameworkCore;

namespace Berber.Models.DatabaseOperations.Operations
{
    public class WorkingHourOp : Generic<WorkingHour>, IWorkingHourOp
    {
        private readonly ApplicationDbContext _appDbContext;
        private readonly DbSet<WorkingHour> workerHourTbl;
        public WorkingHourOp(ApplicationDbContext appDbContext) : base(appDbContext)
        {
            _appDbContext = appDbContext;
            workerHourTbl = _appDbContext.Set<WorkingHour>();
        }
    }
}
