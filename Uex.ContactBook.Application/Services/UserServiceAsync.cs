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
            var newUser = new User(param.UserName);
            AddNotifications(newUser.ValidationResult);

            if (HasNotifications)
                return null;

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
    }
}
