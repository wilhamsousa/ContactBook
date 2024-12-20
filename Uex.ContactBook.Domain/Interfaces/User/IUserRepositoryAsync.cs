using Uex.ContactBook.Domain.Interfaces.Base;
using Uex.ContactBook.Domain.Model.Entities;

namespace Uex.ContactBook.Domain.Interfaces
{
    public interface IUserRepositoryAsync : IAsyncRepositoryBase<User>
    {
        Task<List<User>> GetUserAsync();
        Task<User> GetUserAsync(Guid id);
        Task<User> GetByUserNameAsync(string userName);
    }
}
