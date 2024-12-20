using Uex.ContactBook.Domain.Model.Entities;
using FluentValidation;

namespace Uex.ContactBook.Domain.Model.Validators
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(entity => entity.UserName)
                .NotNull()
                .NotEmpty()
                .WithMessage("Usuário não preenchido.");
        }
    }
}
