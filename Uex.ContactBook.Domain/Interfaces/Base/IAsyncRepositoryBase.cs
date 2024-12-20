namespace Uex.ContactBook.Domain.Interfaces.Base
{
    public interface IAsyncRepositoryBase<TEntity>
    {
        Task<TEntity> GetAsync(Guid id);
        Task<List<TEntity>> GetAsync();
        IQueryable<TEntity> GetQueryable();
        Task<TEntity> CreateAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(TEntity entity);
        Task DeleteAsync(Guid id);
        Dictionary<string, string> MessageErrors();

    }
}
