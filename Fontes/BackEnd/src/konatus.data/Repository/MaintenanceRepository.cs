using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using konatus.business.Interfaces.Repository;
using konatus.business.Models;
using konatus.data.Context;

namespace konatus.data.Repository
{
    public class MaintenanceRepository : Repository<MaintenanceModel>, IMaintenanceRepository
    {
        public MaintenanceRepository(KonatusDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<MaintenanceModel>> GetByUserId(Guid userId)
        {
            return await Find(c => c.UserId == userId);
        }
    }
}