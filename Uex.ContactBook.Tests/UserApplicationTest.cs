using Uex.ContactBook.Application.Services;
using Uex.ContactBook.Domain.Interfaces;
using Uex.ContactBook.Domain.Model.Entities;
using Uex.ContactBook.Domain.Notification;
using Uex.ContactBook.Tests.Base;
using Microsoft.VisualStudio.TestPlatform.Utilities;
using Moq;
using Uex.ContactBook.Application.Services;
using Uex.ContactBook.Domain.Model.Entities;
using Uex.ContactBook.Domain.Notification;
using Uex.ContactBook.Tests.Base;
using Xunit.Abstractions;

namespace Uex.ContactBook.Tests
{
    public class UserApplicationTest : BaseTest
    {
        private readonly Mock<IUserRepositoryAsync> _userRepositoryAsync;
        private readonly IUserServiceAsync _userApplication;
        NotificationContext _notificationContext = new NotificationContext();

        private readonly Guid userId1 = Guid.Parse("8ab7a28f-3526-4abd-8567-7dd42840cbf7");
        private readonly string userName1 = "Usuário 1";

        public UserApplicationTest(ITestOutputHelper output) : base(output)
        {
            _userRepositoryAsync = new Mock<IUserRepositoryAsync>();
            _userApplication = new UserServiceAsync(_userRepositoryAsync.Object, _notificationContext);
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


            _userRepositoryAsync.Setup(x => x
                .GetByNameAsync(It.IsAny<string>()))
                .Callback((string param) => _output.WriteLine($"Received {param}"))
                .Returns(() => Task.FromResult(userResult)
            );
        }

        [Fact]
        public void CreateOk()
        {
            CreateSetup(
                createUserResult: new User(userId1, userName1),
                userResult: null
            );

            var param = new User(userId1, userName1);
            var result = _userApplication.CreateAsync(param).Result;
            Assert.True(result.Valid);
        }

        [Fact]
        public void UserNameAlreadyExists()
        {
            CreateSetup(
                createUserResult: new User(userId1, userName1),
                userResult: new User(userId1, userName1)
            );

            var param = new User(userId1, userName1);
            var result = _userApplication.CreateAsync(param).Result;
            Assert.Null(result);
            Assert.True(_notificationContext.Notifications.Any(x => x.Message == UserMessage.USERNAME_ALREADY_EXISTS));
        }
    }
}
