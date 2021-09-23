using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using konatus.business.Interfaces.Repository;
using konatus.business.Interfaces.Services;
using konatus.business.Models;
using konatus.business.Models.Validations;
using konatus.business.Notifications;

namespace konatus.business.Services
{
    public class StageService : BaseService, IStageService
    {
        private readonly IStageRepository Repository;

        public StageService(IStageRepository repository, INotifier notifier) : base(notifier)
        {
            Repository = repository;
        }

        public async Task<bool> Add(StageModel model)
        {
            if (!ValidationExecute(new StageValidation(), model)) return false;
            await Repository.Add(model);
            return true;
        }

        public async Task<bool> Delete(StageModel model)
        {
            if (!ValidationExecute(new StageValidation(), model)) return false;
            await Repository.Delete(model.Id);
            return true;
        }

        public void Dispose()
        {
            Repository?.Dispose();
        }

        public async Task<IEnumerable<StageModel>> Find(Expression<Func<StageModel, bool>> predicate)
        {
            return await Repository.Find(predicate);
        }

        public async Task<IEnumerable<StageModel>> GetAll()
        {
            return await Repository.GetAll();
        }

        public async Task<IEnumerable<StageModel>> GetByMaintenancetId(Guid maintenanceId)
        {
            return await Repository.GetByMaintenancetId(maintenanceId);
        }

        public async Task<StageModel> GetId(Guid id)
        {
            return (StageModel)await Repository.GetId(id);
        }

        public async Task ChangeState(Guid id)
        {
            var _model = await this.GetId(id);
            await Repository.ChangeState(_model);
        }

        public async Task<bool> Update(StageModel model)
        {
            if (!ValidationExecute(new StageValidation(), model)) return false;
            await Repository.Update(model);
            return true;
        }
    }
}