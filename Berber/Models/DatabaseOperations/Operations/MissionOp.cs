using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System;
using Berber.Models.Tables;
using Berber.Models.Database;

namespace Berber.Models.DatabaseOperations.Operations
{
    public class MissionOp : Generic<Mission>, IMissionOp
    {
        private readonly ApplicationDbContext _appDbContext;
        private readonly DbSet<Mission> missionTbl;
        public MissionOp(ApplicationDbContext appDbContext) : base(appDbContext)
        {
            _appDbContext = appDbContext;
            missionTbl = _appDbContext.Set<Mission>();
        }
    }
}
