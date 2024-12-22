using Uex.ContactBook.Domain.Model.DTOs.User;
using Uex.ContactBook.Domain.Model.Entities;

namespace Uex.ContactBook.Domain.Interfaces
{
    public interface IUserServiceAsync
    {
        Task<User> GetAsync(Guid id);
        Task<User> CreateAsync(UserCreateRequest entity);
        Task DeleteAsync(Guid id);        
    }
}
