using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using konatus.business.Models;

namespace konatus.business.Interfaces.Repository
{
    public interface IMaintenanceRepository : IRepository<MaintenanceModel>
    {
        Task<IEnumerable<MaintenanceModel>> GetByUserId(Guid userId);
    }
}