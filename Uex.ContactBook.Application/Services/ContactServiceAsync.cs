using Uex.ContactBook.Application.Base;
using Uex.ContactBook.Domain.Interfaces;
using Uex.ContactBook.Domain.Model.Entities;
using Uex.ContactBook.Domain.Notification;
using Mapster;
using Uex.ContactBook.Domain.Model.DTOs.Contact;
using RabbitMQ.Client;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace Uex.ContactBook.Application.Services
{
    public class ContactServiceAsync : ServiceBase, IContactServiceAsync
    {
        private readonly IContactRepositoryAsync _contactRepository;
        private IConfiguration _configuration;

        public ContactServiceAsync(
            NotificationContext notificationContext,
            IContactRepositoryAsync contactRepository,
            IConfiguration configuration
        ) : base(notificationContext)
        {
            _contactRepository = contactRepository;
            _configuration = configuration;
        }

        public async Task<Contact> CreateAsync(Guid userId, ContactCreateRequest param)
        {
            var newEntity = param.Adapt<Contact>();
            newEntity.UserId = userId;

            newEntity.Validate();
            AddNotifications(newEntity.ValidationResult);

            if (HasNotifications)
                return newEntity;

            await CpfAlreadyExistsValidation(userId, param.Cpf);

            if (HasNotifications)
                return newEntity;

            await EmailAlreadyExistsValidation(userId, param.Email);

            if (HasNotifications)
                return newEntity;

            var result = await _contactRepository.CreateAsync(newEntity);
            result.Validate();
            return result;
        }

        public async Task UpdateAsync(Guid userId, Guid id, ContactUpdateRequest param)
        {
            var newEntity = param.Adapt<Contact>();
            newEntity.UserId = userId;

            newEntity.Validate();
            AddNotifications(newEntity.ValidationResult);

            if (HasNotifications)
                return;

            await IdNullValidation(id);

            newEntity.SetId(id);

            if (HasNotifications)
                return;

            await CpfAlreadyExistsValidation(userId, id, param.Cpf);

            if (HasNotifications)
                return;

            await EmailAlreadyExistsValidation(userId, id, param.Email);

            if (HasNotifications)
                return;

            await _contactRepository.UpdateAsync(newEntity);
        }

        private async Task CpfAlreadyExistsValidation(Guid userId, string cpf)
        {
            var entity = await _contactRepository.GetByCpfAsync(userId, cpf);

            if (entity != null)
                AddValidationFailure(ContactMessage.CPF_ALREADY_EXISTS);
        }

        private async Task CpfAlreadyExistsValidation(Guid userId, Guid id, string cpf)
        {
            var oldEntity = await _contactRepository.GetAsync(id);
            if (cpf.Equals(oldEntity.Cpf))
                return;

            var entity = await _contactRepository.GetByCpfAsync(userId, cpf);

            if (entity != null)
                AddValidationFailure(ContactMessage.CPF_ALREADY_EXISTS);
        }

        private async Task EmailAlreadyExistsValidation(Guid userId, Guid id, string email)
        {
            var oldEntity = await _contactRepository.GetAsync(id);
            if (email.Equals(oldEntity.Email))
                return;

            var entity = await _contactRepository.GetByEmailAsync(userId, email);

            if (entity != null)
                AddValidationFailure(ContactMessage.EMAIL_ALREADY_EXISTS);
        }

        private async Task EmailAlreadyExistsValidation(Guid userId, string email)
        {
            var entity = await _contactRepository.GetByEmailAsync(userId, email);

            if (entity != null)
                AddValidationFailure(ContactMessage.EMAIL_ALREADY_EXISTS);
        }

        private async Task IdNullValidation(Guid id)
        {
            if (id == null || id == Guid.Empty)
                AddValidationFailure(ContactMessage.ID_IS_EMPTY);
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
            var entity = await _contactRepository.GetByCpfAsync(userId, name);
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

        public async Task SendEmail(Guid userId, ContactSendEmailMessageRequest param)
        {
            var entities = await GetAsync(userId);
            if (entities == null)
            {
                AddValidationFailure(ContactMessage.CONTACT_NOTFOUND);
                return;
            }

            foreach (var item in entities)
            {
                SendEmail(item.Email, param.message);
            }
        }

        private async void SendEmail(string email, string message)
        {
            Console.WriteLine(email, message);

            var factory = new ConnectionFactory()
            {
                HostName = _configuration.GetSection("Rabbitmq:HostName").Value,
                Port = Convert.ToInt16(_configuration.GetSection("Rabbitmq:Port").Value ?? "0"),
                UserName = _configuration.GetSection("Rabbitmq:Username").Value,
                Password = _configuration.GetSection("Rabbitmq:Password").Value
            };

            var connection = await factory.CreateConnectionAsync();
            string queueName = "queueName";

            var channel = await connection.CreateChannelAsync();
            await channel.QueueDeclareAsync(
                queue: queueName,
                exclusive: false,
                autoDelete: false,
                arguments: null
            );

            var body = Encoding.UTF8.GetBytes(message);

            await channel.BasicPublishAsync(
                exchange: string.Empty,
                routingKey: queueName,
                body: body
            );
        }
    }
}
