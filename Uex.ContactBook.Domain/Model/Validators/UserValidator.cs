using Uex.ContactBook.Domain.Model.Entities;
using FluentValidation;

namespace Uex.ContactBook.Domain.Model.Validators
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(entity => entity.UserName)
                .NotEmpty()
                .WithMessage(UserMessage.USERNAME_NOT_INFORMED)
                .MinimumLength(10)
                .WithMessage(UserMessage.USERNAME_DIGITS);

            RuleFor(entity => entity.Email)
                .NotEmpty()
                .WithMessage(UserMessage.EMAIL_NOT_INFORMED)
                .EmailAddress()
                .WithMessage(UserMessage.EMAIL_INVALID);

            RuleFor(entity => entity.Password)
                .NotEmpty()
                .WithMessage(UserMessage.PASSWORD_NOT_INFORMED)
                .MinimumLength(6)
                .WithMessage(UserMessage.PASSWORD_DIGITS);
        }
    }
}
