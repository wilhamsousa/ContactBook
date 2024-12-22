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

        public async Task<Contact> CreateAsync(ContactCreateRequest param)
        {
            var newEntity = param.Adapt<Contact>();
            newEntity.Validate();
            AddNotifications(newEntity.ValidationResult);

            if (HasNotifications)
                return newEntity;

            await NameAlreadyExistsValidation(param.Name);

            if (HasNotifications)
                return newEntity;

            await EmailAlreadyExistsValidation(param.Email);

            if (HasNotifications)
                return newEntity;

            var result = await _contactRepository.CreateAsync(newEntity);
            result.Validate();
            return result;
        }

        private async Task NameAlreadyExistsValidation(string name)
        {
            var entity = await _contactRepository.GetByNameAsync(name);

            if (entity != null)
                AddValidationFailure(ContactMessage.NAME_ALREADY_EXISTS);
        }

        private async Task EmailAlreadyExistsValidation(string email)
        {
            var entity = await _contactRepository.GetByEmailAsync(email);

            if (entity != null)
                AddValidationFailure(ContactMessage.EMAIL_ALREADY_EXISTS);
        }

        public async Task<ContactGetResponse> GetAsync(Guid id)
        {
            var entity = await _contactRepository.GetContactAsync(id);
            var model = entity.Adapt<ContactGetResponse>();
            return model;
        }

        public async Task<IEnumerable<ContactGetAllResponse>> GetAsync()
        {
            var entity = await _contactRepository.GetContactAsync();
            var model = entity.Adapt<IEnumerable<ContactGetAllResponse>>();
            return model;
        }

        public async Task<Contact> GetByNameAsync(string name)
        {
            var entity = await _contactRepository.GetByNameAsync(name);
            return entity;
        }

        public virtual async Task DeleteAsync(Guid id)
        {
            var entity = await GetAsync(id);
            if (entity == null)
            {
                AddValidationFailure(ContactMessage.CONTACT_NOTFOUND);
                return;
            }

            await _contactRepository.DeleteAsync(id);
        }        
    }
}
