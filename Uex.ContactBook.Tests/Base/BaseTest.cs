using Uex.ContactBook.Domain.Notification;
using Xunit.Abstractions;

namespace Uex.ContactBook.Tests.Base
{
    public abstract class BaseTest
    {
        private readonly NotificationContext _notificationContext = new NotificationContext();
        protected readonly ITestOutputHelper _output;

        protected BaseTest(ITestOutputHelper output)
        {
            _output = output;
        }
    }
}
