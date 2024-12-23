using Uex.ContactBook.Domain.Interfaces.Base;
using Uex.ContactBook.Domain.Model.Entities;

namespace Uex.ContactBook.Domain.Interfaces
{
    public interface IContactRepositoryAsync : IAsyncRepositoryBase<Contact>
    {
        Task<List<Contact>> GetContactAsync(Guid userId);
        Task<Contact> GetContactAsync(Guid userId, Guid id);
        Task<Contact> GetByCpfAsync(Guid userId, string cpf);
        Task<Contact> GetByEmailAsync(Guid userId, string email);
    }
}
