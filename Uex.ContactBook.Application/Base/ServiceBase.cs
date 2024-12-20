using Uex.ContactBook.Domain.Notification;
using FluentValidation.Results;

namespace Uex.ContactBook.Application.Base
{
    public abstract class ServiceBase
    {
        private readonly ValidationResult _validationResult = new ValidationResult();
        private readonly NotificationContext _notificationContext;

        public bool HasNotifications { get => _notificationContext.HasNotifications; }

        protected ServiceBase(NotificationContext notificationContext)
        {
            _notificationContext = notificationContext;
        }

        protected void AddValidationFailure(string message)
        {
            _validationResult.Errors.Add(new ValidationFailure() { ErrorMessage = message });
            _notificationContext.AddNotifications(_validationResult);
        }

        protected void AddNotifications(ValidationResult validationResult)
        {
            if (validationResult != null && validationResult.Errors.Any())
                _notificationContext.AddNotifications(validationResult);
        }
    }
}
