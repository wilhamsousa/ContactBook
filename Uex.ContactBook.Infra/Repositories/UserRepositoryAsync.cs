using Uex.ContactBook.Domain.Interfaces;
using Uex.ContactBook.Domain.Model.Entities;
using Uex.ContactBook.Domain.Model.Enums;
using Uex.ContactBook.Domain.Notification;
using Uex.ContactBook.Infra.Base;
using Uex.ContactBook.Infra.Repositories.Context;
using Microsoft.EntityFrameworkCore;
using Azure;
using Microsoft.Extensions.Logging;

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

        public override async Task DeleteAsync(Guid id)
        {
            using (var objTrans = _context.Database.BeginTransaction())
            {
                try
                {
                    await _context.Contact
                        .Where(x => x.UserId == id)
                        .ExecuteDeleteAsync();
                    await _context.SaveChangesAsync();

                    var entity = await _context.User.FindAsync(id);
                    await DeleteAsync(entity);

                    objTrans.Commit();
                }
                catch (Exception e)
                {
                    objTrans.Rollback();
                    AddValidationFailure("Erro ao excluir usuário ou seus contatos.");
                }
            }
        }
    }
}
