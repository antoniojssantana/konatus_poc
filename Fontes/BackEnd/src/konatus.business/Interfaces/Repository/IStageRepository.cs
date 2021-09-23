using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using konatus.business.Models;

namespace konatus.business.Interfaces.Repository
{
    public interface IStageRepository : IRepository<StageModel>
    {
        Task<IEnumerable<StageModel>> GetByMaintenancetId(Guid maintenanceId);
    }
}