using Uex.ContactBook.Domain.Interfaces.Base;
using Uex.ContactBook.Domain.Model.Entities;

namespace Uex.ContactBook.Domain.Interfaces
{
    public interface IContactRepositoryAsync : IAsyncRepositoryBase<Contact>
    {
        Task<List<Contact>> GetContactAsync();
        Task<Contact> GetContactAsync(Guid id);
        Task<Contact> GetByNameAsync(string name);
        Task<Contact> GetByEmailAsync(string email);
    }
}
