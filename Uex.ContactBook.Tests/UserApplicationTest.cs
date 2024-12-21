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

        private readonly Guid userId1 = Guid.Parse("8ab7a28f-3526-4abd-8567-7dd42840cbf7");
        private readonly string userName1 = "Usu�rio 1";

        public UserApplicationTest(ITestOutputHelper output) : base(output)
        {
            _userRepositoryAsync = new Mock<IUserRepositoryAsync>();
            _userServiceAsync = new UserServiceAsync(_notificationContext, _userRepositoryAsync.Object);
        }

        private void CreateSetup(
            User createUserResult,
            User userResult
        )
        {
            _userRepositoryAsync.Setup(x => x
                .CreateAsync(It.IsAny<User>()))
                .Callback((User param) => _output.WriteLine($"Received {param.Id}"))
                .Returns(() => Task.FromResult(createUserResult)
            );
        }

        [Fact]
        public void CreateOk()
        {
            CreateSetup(
                createUserResult: new User(userName1),
                userResult: null
            );

            var param = new UserCreateRequest(userName1);
            var result = _userServiceAsync.CreateAsync(param).Result;
            Assert.True(result.Valid);
        }

        [Fact]
        public void UserNameAlreadyExists()
        {
            CreateSetup(
                createUserResult: new User(userName1),
                userResult: new User(userName1)
            );

            var param = new UserCreateRequest(userName1);
            var result = _userServiceAsync.CreateAsync(param).Result;
            Assert.Null(result);
            Assert.True(_notificationContext.Notifications.Any(x => x.Message == UserMessage.USER_USERNAME_ALREADY_EXISTS));
        }
    }
}
