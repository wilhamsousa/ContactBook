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
            var newUser = param.Adapt<User>();
            AddNotifications(newUser.ValidationResult);

            if (HasNotifications)
                return newUser;

            UserAlreadyExistsValidation(param.UserName);

            if (HasNotifications)
                return null;

            var result = await _userRepository.CreateAsync(newUser); ;
            return result;
        }

        private async Task UserAlreadyExistsValidation(string userName)
        {
            var user = await _userRepository.GetByUserNameAsync(userName);

            if (user != null)
                AddValidationFailure(UserMessage.USER_USERNAME_ALREADY_EXISTS);
        }

        public async Task<UserGetResponse> GetAsync(Guid id)
        {
            var UserEntity = await _userRepository.GetUserAsync(id);
            var model = UserEntity.Adapt<UserGetResponse>();
            return model;
        }

        public async Task<IEnumerable<UserGetAllResponse>> GetAsync()
        {
            var entity = await _userRepository.GetUserAsync();
            var model = entity.Adapt<IEnumerable<UserGetAllResponse>>();
            return model;
        }

        public async Task<User> GetByUserNameAsync(string userName)
        {
            var entity = await _userRepository.GetByUserNameAsync(userName);
            return entity;
        }

        public virtual async Task DeleteAsync(Guid id)
        {
            await _userRepository.DeleteAsync(id);
        }
    }
}
