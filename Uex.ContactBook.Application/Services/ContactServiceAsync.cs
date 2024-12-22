using Uex.ContactBook.Application.Base;
using Uex.ContactBook.Domain.Interfaces;
using Uex.ContactBook.Domain.Model.Entities;
using Uex.ContactBook.Domain.Notification;
using Mapster;
using Uex.ContactBook.Domain.Model.DTOs.Contact;

namespace Uex.ContactBook.Application.Services
{
    public class ContactServiceAsync : ServiceBase, IContactServiceAsync
    {
        private readonly IContactRepositoryAsync _contactRepository;

        public ContactServiceAsync(
            NotificationContext notificationContext,
            IContactRepositoryAsync contactRepository
        ) : base(notificationContext)
        {
            _contactRepository = contactRepository;
        }

        public async Task<Contact> CreateAsync(Guid userId, ContactCreateRequest param)
        {
            var newEntity = param.Adapt<Contact>();
            newEntity.UserId = userId;

            newEntity.Validate();
            AddNotifications(newEntity.ValidationResult);

            if (HasNotifications)
                return newEntity;

            await NameAlreadyExistsValidation(userId, param.Name);

            if (HasNotifications)
                return newEntity;

            await EmailAlreadyExistsValidation(userId, param.Email);

            if (HasNotifications)
                return newEntity;

            var result = await _contactRepository.CreateAsync(newEntity);
            result.Validate();
            return result;
        }

        private async Task NameAlreadyExistsValidation(Guid userId, string name)
        {
            var entity = await _contactRepository.GetByNameAsync(userId, name);

            if (entity != null)
                AddValidationFailure(ContactMessage.NAME_ALREADY_EXISTS);
        }

        private async Task EmailAlreadyExistsValidation(Guid userId, string email)
        {
            var entity = await _contactRepository.GetByEmailAsync(userId, email);

            if (entity != null)
                AddValidationFailure(ContactMessage.EMAIL_ALREADY_EXISTS);
        }

        public async Task<Contact> GetAsync(Guid userId, Guid id)
        {
            var entity = await _contactRepository.GetContactAsync(userId, id);
            return entity;
        }

        public async Task<IEnumerable<Contact>> GetAsync(Guid userId)
        {
            var entity = await _contactRepository.GetContactAsync(userId);
            return entity;
        }

        public async Task<Contact> GetByNameAsync(Guid userId, string name)
        {
            var entity = await _contactRepository.GetByNameAsync(userId, name);
            return entity;
        }

        public virtual async Task DeleteAsync(Guid userId, Guid id)
        {
            var entity = await GetAsync(userId, id);
            if (entity == null)
            {
                AddValidationFailure(ContactMessage.CONTACT_NOTFOUND);
                return;
            }

            await _contactRepository.DeleteAsync(id);
        }        
    }
}
