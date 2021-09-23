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
    public class MaintenanceService : BaseService, IMaintenanceService
    {
        private readonly IMaintenanceRepository Repository;

        public MaintenanceService(IMaintenanceRepository repository, INotifier notifier) : base(notifier)
        {
            Repository = repository;
        }

        public async Task<bool> Add(MaintenanceModel model)
        {
            if (!ValidationExecute(new MaintenanceValidation(), model)) return false;
            await Repository.Add(model);
            return true;
        }

        public async Task<bool> Delete(MaintenanceModel model)
        {
            if (!ValidationExecute(new MaintenanceValidation(), model)) return false;
            await Repository.Delete(model.Id);
            return true;
        }

        public void Dispose()
        {
            Repository?.Dispose();
        }

        public async Task<IEnumerable<MaintenanceModel>> Find(Expression<Func<MaintenanceModel, bool>> predicate)
        {
            return await Repository.Find(predicate);
        }

        public async Task<IEnumerable<MaintenanceModel>> GetAll()
        {
            return await Repository.GetAll();
        }

        public async Task<IEnumerable<MaintenanceModel>> GetByUserId(Guid userId)
        {
            return await Repository.GetByUserId(userId);
        }

        public async Task<MaintenanceModel> GetId(Guid id)
        {
            return (MaintenanceModel)await Repository.GetId(id);
        }

        public async Task ChangeState(Guid id)
        {
            var _model = await this.GetId(id);
            await Repository.ChangeState(_model);
        }

        public async Task<bool> Update(MaintenanceModel model)
        {
            if (!ValidationExecute(new MaintenanceValidation(), model)) return false;
            await Repository.Update(model);
            return true;
        }
    }
}