using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using konatus.business.Models;

namespace konatus.business.Interfaces.Services
{
    public interface IMaintenanceService : IDisposable
    {
        Task<IEnumerable<MaintenanceModel>> GetAll();

        Task<MaintenanceModel> GetId(Guid id);

        Task<IEnumerable<MaintenanceModel>> GetByUserId(Guid userId);

        Task<IEnumerable<MaintenanceModel>> Find(Expression<Func<MaintenanceModel, bool>> predicate);

        Task ChangeState(Guid id);

        Task<bool> Add(MaintenanceModel model);

        Task<bool> Update(MaintenanceModel model);

        Task<bool> Delete(MaintenanceModel model);
    }
}