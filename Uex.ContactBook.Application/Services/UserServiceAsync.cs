using Uex.ContactBook.Application.Base;
using Uex.ContactBook.Domain.Interfaces;
using Uex.ContactBook.Domain.Model.Entities;
using Uex.ContactBook.Domain.Notification;
using Mapster;
using Uex.ContactBook.Domain.Model.DTOs.User;

namespace Uex.ContactBook.Application.Services
{
    public class UserServiceAsync : ServiceBase, IUserServiceAsync
    {
        private readonly IUserRepositoryAsync _userRepository;

        public UserServiceAsync(
            NotificationContext notificationContext,
            IUserRepositoryAsync userRepository
        ) : base(notificationContext)
        {
            _userRepository = userRepository;
        }

        public async Task<User> CreateAsync(UserCreateRequest param)
        {
            var newEntity = param.Adapt<User>();
            newEntity.Validate();
            AddNotifications(newEntity.ValidationResult);

            if (HasNotifications)
                return newEntity;

            await UserNameAlreadyExistsValidation(param.UserName);

            if (HasNotifications)
                return newEntity;

            await EmailAlreadyExistsValidation(param.Email);

            if (HasNotifications)
                return newEntity;

            var result = await _userRepository.CreateAsync(newEntity);
            result.Validate();
            return result;
        }

        private async Task UserNameAlreadyExistsValidation(string userName)
        {
            var entity = await _userRepository.GetByUserNameAsync(userName);

            if (entity != null)
                AddValidationFailure(UserMessage.USER_USERNAME_ALREADY_EXISTS);
        }

        private async Task EmailAlreadyExistsValidation(string email)
        {
            var entity = await _userRepository.GetByEmailAsync(email);

            if (entity != null)
                AddValidationFailure(UserMessage.USER_EMAIL_ALREADY_EXISTS);
        }

        public async Task<UserGetResponse> GetAsync(Guid id)
        {
            var entity = await _userRepository.GetUserAsync(id);
            var model = entity.Adapt<UserGetResponse>();
            return model;
        }

        public virtual async Task DeleteAsync(Guid id)
        {
            var entity = await GetAsync(id);
            if (entity == null)
            {
                AddValidationFailure(UserMessage.USER_NOTFOUND);
                return;
            }

            await _userRepository.DeleteAsync(id);
        }        
    }
}
