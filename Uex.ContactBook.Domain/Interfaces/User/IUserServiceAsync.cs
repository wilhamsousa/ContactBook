using Uex.ContactBook.Domain.Model.DTOs.User;
using Uex.ContactBook.Domain.Model.Entities;

namespace Uex.ContactBook.Domain.Interfaces
{
    public interface IUserServiceAsync
    {
        Task<UserGetResponse> GetAsync(Guid id);
        Task<IEnumerable<UserGetAllResponse>> GetAsync();
        Task<User> CreateAsync(UserCreateRequest entity);

    }
}
