using Uex.ContactBook.Application.Services;
using Uex.ContactBook.Domain.Interfaces;
using Uex.ContactBook.Domain.Model.Entities;
using Uex.ContactBook.Domain.Notification;
using Uex.ContactBook.Tests.Base;
using Moq;
using Xunit.Abstractions;
using Uex.ContactBook.Domain.Model.DTOs.User;

namespace Uex.ContactBook.Tests
{
    public class UserApplicationTest : BaseTest
    {
        private readonly Mock<IUserRepositoryAsync> _userRepositoryAsync;
        private readonly IUserServiceAsync _userServiceAsync;
        NotificationContext _notificationContext = new NotificationContext();

        private readonly string newUsername = "Wilham Ezequiel de Sousa";
        private readonly string usernameInDatabase = "Joao Antonio";
        private readonly string invalidNewoUsernameLengh = "Wilham";
        private readonly string validEmail = "email@dominio.com";
        private readonly string password = "123456";

        public UserApplicationTest(ITestOutputHelper output) : base(output)
        {
            _userRepositoryAsync = new Mock<IUserRepositoryAsync>();
            _userServiceAsync = new UserServiceAsync(_notificationContext, _userRepositoryAsync.Object);
        }

        private void UserCreateRepositorySetup(
            User response
        )
        {
            _userRepositoryAsync.Setup(x => x
                .CreateAsync(It.IsAny<User>()))
                .Callback((User response) => _output.WriteLine($"UserCreateRepository Response: {response.Id}"))
                .Returns(() => Task.FromResult(response)
            );
        }

        private void GetByUserNameRepositorySetup(
            User response
        )
        {
            _userRepositoryAsync.Setup(x => x
                .GetByUserNameAsync(It.IsAny<string>()))
                .Callback((string response) => _output.WriteLine($"GetByUserNameRepository Response {response}"))
                .Returns(() => Task.FromResult(response)
            );
        }

        [Fact]
        public void CreateOk()
        {
            UserCreateRepositorySetup(
                response: new User(usernameInDatabase, validEmail, password)
            );

            GetByUserNameRepositorySetup(
                response: new User(usernameInDatabase, validEmail, password)
            );

            var param = new UserCreateRequest(newUsername, validEmail, password);
            var result = _userServiceAsync.CreateAsync(param).Result;
            Assert.True(result.Valid, result.NotificationMessage());
        }

        [Fact]
        public void CreateInvalidUserNameError()
        {
            UserCreateRepositorySetup(
                response: new User(usernameInDatabase, validEmail, password)
            );

            GetByUserNameRepositorySetup(
                response: new User(usernameInDatabase, validEmail, password)
            );

            var param = new UserCreateRequest(invalidNewoUsernameLengh, validEmail, password);
            var result = _userServiceAsync.CreateAsync(param).Result;
            Assert.False(result.Valid, result.NotificationMessage());
        }

        [Fact]
        public void UserNameAlreadyExists()
        {
            UserCreateRepositorySetup(
                response: new User(usernameInDatabase, validEmail, password)
            );

            GetByUserNameRepositorySetup(
                response: new User(usernameInDatabase, validEmail, password)
            );

            var param = new UserCreateRequest(usernameInDatabase, validEmail, password);
            var result = _userServiceAsync.CreateAsync(param).Result;
            Assert.True(_notificationContext.Notifications.Any(x => x.Message == UserMessage.USER_USERNAME_ALREADY_EXISTS), result.NotificationMessage());
        }
    }
}
