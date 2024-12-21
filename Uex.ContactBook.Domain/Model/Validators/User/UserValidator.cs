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
                .WithMessage("Usuário não preenchido.")
                .MinimumLength(10)
                .WithMessage("Usuário deve conter pelo menos 10 dígitos.");
        }
    }
}
