using Uex.ContactBook.Domain.Model.DTOs.Contact;
using Uex.ContactBook.Domain.Model.Entities;

namespace Uex.ContactBook.Domain.Interfaces
{
    public interface IContactServiceAsync
    {
        Task<ContactGetResponse> GetAsync(Guid id);
        Task<IEnumerable<ContactGetAllResponse>> GetAsync();
        Task<Contact> GetByNameAsync(string name);
        Task<Contact> CreateAsync(ContactCreateRequest entity);
        Task DeleteAsync(Guid id);        
    }
}
