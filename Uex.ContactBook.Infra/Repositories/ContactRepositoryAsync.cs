using Uex.ContactBook.Domain.Interfaces;
using Uex.ContactBook.Domain.Model.Entities;
using Uex.ContactBook.Domain.Notification;
using Uex.ContactBook.Infra.Base;
using Uex.ContactBook.Infra.Repositories.Context;
using Microsoft.EntityFrameworkCore;

namespace Uex.ContactBook.Infra.Repositories
{
    public class ContactRepositoryAsync : RepositoryBaseAsync<Contact>, IContactRepositoryAsync
    {
        public ContactRepositoryAsync(ContactBookContext context, NotificationContext notificationContext) : base(context, notificationContext)
        {
        }

        public async Task<List<Contact>> GetContactAsync(Guid userId)
        {
            var entities = await _context.Contact
                .Where(x => x.UserId == userId)
                .ToListAsync();

            return entities;
        }

        public async Task<Contact> GetContactAsync(Guid userId, Guid id)
        {
            var entity = await _context.Contact
                .Where(x => x.UserId == userId)
                .Where(x => x.Id == id)
                .SingleOrDefaultAsync();

            return entity;
        }

        public async Task<Contact> GetByNameAsync(Guid userId, string name)
        {
            var entity = await _context.Contact
                .Where(x => x.UserId == userId)
                .Where(x => x.Name == name)
                .SingleOrDefaultAsync();

            return entity;
        }

        public async Task<Contact> GetByEmailAsync(Guid userId, string email)
        {
            var entity = await _context.Contact
                .Where(x => x.UserId == userId)
                .Where(x => x.Email == email)
                .SingleOrDefaultAsync();

            return entity;
        }
    }
}
