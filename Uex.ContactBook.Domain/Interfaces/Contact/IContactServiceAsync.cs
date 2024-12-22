using Uex.ContactBook.Domain.Model.DTOs.Contact;
using Uex.ContactBook.Domain.Model.Entities;

namespace Uex.ContactBook.Domain.Interfaces
{
    public interface IContactServiceAsync
    {
        Task<Contact> GetAsync(Guid userId, Guid id);
        Task<IEnumerable<Contact>> GetAsync(Guid userId);
        Task<Contact> GetByNameAsync(Guid userId, string name);
        Task<Contact> CreateAsync(Guid userId, ContactCreateRequest entity);
        Task DeleteAsync(Guid userId, Guid id);        
    }
}
