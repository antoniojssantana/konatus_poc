using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using konatus.business.Interfaces.Repository;
using konatus.business.Models;
using konatus.data.Context;

namespace konatus.data.Repository
{
    public class StageRepository : Repository<StageModel>, IStageRepository
    {
        public StageRepository(KonatusDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<StageModel>> GetByMaintenancetId(Guid maintenanceId)
        {
            return await Find(c => c.MaintenanceId == maintenanceId);
        }
    }
}