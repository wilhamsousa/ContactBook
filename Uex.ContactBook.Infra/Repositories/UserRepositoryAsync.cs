using Uex.ContactBook.Domain.Interfaces;
using Uex.ContactBook.Domain.Model.Entities;
using Uex.ContactBook.Domain.Model.Enums;
using Uex.ContactBook.Domain.Notification;
using Uex.ContactBook.Infra.Base;
using Uex.ContactBook.Infra.Repositories.Context;
using Microsoft.EntityFrameworkCore;

namespace Uex.ContactBook.Infra.Repositories
{
    public class UserRepositoryAsync : RepositoryBaseAsync<User>, IUserRepositoryAsync
    {
        public UserRepositoryAsync(ContactBookContext context, NotificationContext notificationContext) : base(context, notificationContext)
        {
        }

        public async Task<List<User>> GetUserAsync()
        {
            var entities = await _context.User
                .ToListAsync();

            return entities;
        }

        public async Task<User> GetUserAsync(Guid id)
        {
            var entity = await _context.User
                .Where(x => x.Id == id)
                .SingleOrDefaultAsync();

            return entity;
        }

        public async Task<User> GetByUserNameAsync(string userName)
        {
            var entity = await _context.User
                .Where(x => x.UserName == userName)
                .SingleOrDefaultAsync();

            return entity;
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            var entity = await _context.User
                .Where(x => x.Email == email)
                .SingleOrDefaultAsync();

            return entity;
        }
    }
}
