using Uex.ContactBook.Domain.Model.DTOs.User;
using FluentValidation;

namespace Uex.ContactBook.Domain.Model.Validators
{
    public sealed class UserCreateRequestValidator : AbstractValidator<UserCreateRequest>
    {
        public UserCreateRequestValidator()
        {
            RuleFor(entity => entity.UserName)
                .NotNull()
                .NotEmpty()
                .WithMessage("Usuário não preenchido.");
        }
    }
}
