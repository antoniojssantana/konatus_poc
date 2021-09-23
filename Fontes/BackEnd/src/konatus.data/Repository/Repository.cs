using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using konatus.business.Interfaces.Repository;
using konatus.business.Models;
using konatus.data.Context;

namespace konatus.data.Repository
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity, new()
    {
        protected readonly KonatusDbContext _DbContext;
        protected readonly DbSet<TEntity> _DbSet;

        protected Repository(KonatusDbContext dbContext)
        {
            _DbContext = dbContext;
            _DbSet = _DbContext.Set<TEntity>();
        }

        public async Task Add(TEntity entity)
        {
            _DbSet.Add(entity);
            await this.SaveChanges();
        }

        public async Task Delete(Guid id)
        {
            _DbSet.Remove(new TEntity { Id = id });
            await this.SaveChanges();
        }

        public async Task<IEnumerable<TEntity>> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return await _DbSet.AsNoTracking().Where(predicate).ToListAsync();
        }

        public virtual async Task<IEnumerable<TEntity>> GetAll()
        {
            return await _DbSet.ToListAsync();
        }

        public virtual async Task<Entity> GetId(Guid id)
        {
            return await _DbSet.FindAsync(id);
        }

        public virtual async Task Update(TEntity entity)
        {
            _DbSet.Update(entity);
            await this.SaveChanges();
        }

        public virtual async Task ChangeState(TEntity entity)
        {
            switch (entity.Status)
            {
                case business.Enums.StatusDefault.Active:
                    entity.Status = business.Enums.StatusDefault.Inactive;
                    break;

                case business.Enums.StatusDefault.Inactive:
                    entity.Status = business.Enums.StatusDefault.Active;
                    break;

                default:
                    entity.Status = business.Enums.StatusDefault.Active;
                    break;
            }
            await this.Update(entity);
        }

        public async Task<int> SaveChanges()
        {
            return await _DbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            _DbContext?.Dispose();
        }
    }
}