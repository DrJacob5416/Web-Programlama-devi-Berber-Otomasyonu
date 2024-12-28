using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System;
using Berber.Models.Tables;
using Berber.Models.Database;

namespace Berber.Models.DatabaseOperations.Operations
{
    public class YapayZekaOp : Generic<YapayZeka>, IYapayZekaOp
    {
        private readonly ApplicationDbContext _appDbContext;
        private readonly DbSet<YapayZeka> yapayzekatbl;
        public YapayZekaOp(ApplicationDbContext appDbContext) : base(appDbContext)
        {
            _appDbContext = appDbContext;
            yapayzekatbl = _appDbContext.Set<YapayZeka>();
        }
    }
}
