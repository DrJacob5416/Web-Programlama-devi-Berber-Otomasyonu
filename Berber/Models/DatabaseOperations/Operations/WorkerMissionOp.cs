using Berber.Models.Database;
using Berber.Models.Tables;
using Microsoft.EntityFrameworkCore;

namespace Berber.Models.DatabaseOperations.Operations
{
    public class WorkerMissionOp : Generic<WorkerMission>, IWorkerMissionOp
    {
        private readonly ApplicationDbContext _appDbContext;
        private readonly DbSet<WorkerMission> workerMissionTbl;
        public WorkerMissionOp(ApplicationDbContext appDbContext) : base(appDbContext)
        {
            _appDbContext = appDbContext;
            workerMissionTbl = _appDbContext.Set<WorkerMission>();
        }
    }
}
