using FluentValidation.Results;
using Uex.ContactBook.Domain.Model.Base;
using Uex.ContactBook.Domain.Notification;
using Uex.ContactBook.Infra.Repositories.Context;
using Microsoft.EntityFrameworkCore;
using Uex.ContactBook.Domain.Interfaces.Base;

namespace Uex.ContactBook.Infra.Base
{
    public abstract class RepositoryBaseAsync<TEntity> : IAsyncRepositoryBase<TEntity>
        where TEntity : BaseEntity
    {
        public readonly ContactBookContext _context;
        private readonly ValidationResult _validationResult = new ValidationResult();
        private readonly NotificationContext _notificationContext;

        protected RepositoryBaseAsync(ContactBookContext context, NotificationContext notificationContext)
        {
            _context = context;
            _notificationContext = notificationContext;
        }

        public virtual async Task<TEntity> GetAsync(Guid id)
        {
            var entity = await _context.Set<TEntity>().FindAsync(id);
            if (entity != null)
                _context.Entry(entity).State = EntityState.Detached;

            return entity;
        }

        public Task<List<TEntity>> GetAsync() =>
            _context.Set<TEntity>().ToListAsync();

        public IQueryable<TEntity> GetQueryable() =>
            _context.Set<TEntity>().AsNoTracking().AsQueryable();

        public virtual async Task<TEntity> CreateAsync(TEntity param)
        {
            CreateId(param);

            var data = _context.Set<TEntity>().Add(param);
            var result = await _context.SaveChangesAsync();
            return data.Entity;
        }

        public virtual async Task<TEntity> CreateOrUpdateAsync(TEntity entity)
        {
            if (entity.Id != Guid.Empty)
            {
                _context.Entry<TEntity>(entity).State = EntityState.Detached;
                _context.Set<TEntity>().Update(entity);
                var data = _context.Set<TEntity>().Update(entity);
                await _context.SaveChangesAsync();
                return data.Entity;
            }
            else
            {
                CreateId(entity);
                var data = _context.Set<TEntity>().Add(entity);
                await _context.SaveChangesAsync();
                return data.Entity;
            }
        }

        public virtual Task UpdateAsync(TEntity entity)
        {
            var local = _context.Set<TEntity>().Local.FirstOrDefault(entry => entry.Id.Equals(entity.Id));
            if (local != null)
                _context.Entry<TEntity>(local).State = EntityState.Detached;

            _context.Entry<TEntity>(entity).State = EntityState.Modified;
            var result = _context.Set<TEntity>().Update(entity);
            return _context.SaveChangesAsync();
        }

        public virtual Task DeleteAsync(TEntity param)
        {
            _context.Entry<TEntity>(param).State = EntityState.Detached;
            _context.Set<TEntity>().Remove(param);
            return _context.SaveChangesAsync();
        }

        public virtual async Task DeleteAsync(Guid id)
        {
            var entity = await _context.Set<TEntity>().FindAsync(id);
            await DeleteAsync(entity);
        }

        private void CreateId(TEntity param)
        {
            if (param.Id == Guid.Empty)
                param.SetId(Guid.NewGuid());
        }

        public virtual Dictionary<string, string> MessageErrors() =>
            new Dictionary<string, string>();

        protected void AddValidationFailure(string message)
        {
            _validationResult.Errors.Add(new ValidationFailure() { ErrorMessage = message });
            _notificationContext.AddNotifications(_validationResult);
        }
    }
}
