using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using konatus.business.Models;

namespace konatus.business.Interfaces.Services
{
    public interface IStageService : IDisposable
    {
        Task<IEnumerable<StageModel>> GetAll();

        Task<StageModel> GetId(Guid id);

        Task<IEnumerable<StageModel>> GetByMaintenancetId(Guid maintenanceId);

        Task<IEnumerable<StageModel>> Find(Expression<Func<StageModel, bool>> predicate);

        Task ChangeState(Guid id);

        Task<bool> Add(StageModel model);

        Task<bool> Update(StageModel model);

        Task<bool> Delete(StageModel model);
    }
}