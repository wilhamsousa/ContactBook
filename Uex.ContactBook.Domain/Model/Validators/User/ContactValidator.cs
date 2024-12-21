using Uex.ContactBook.Domain.Model.Entities;
using FluentValidation;
using Uex.ContactBook.Domain.Extensions;
using Uex.ContactBook.Domain.Model.Consts;

namespace Uex.ContactBook.Domain.Model.Validators
{
    public class ContactValidator : AbstractValidator<Contact>
    {
        public ContactValidator()
        {
            RuleFor(entity => entity.Name)
                .NotEmpty()
                .WithMessage("Nome não preenchido.")
                .MinimumLength(10)
                .WithMessage("Nome deve conter pelo menos 10 dígitos.");

            RuleFor(entity => entity.Email)
                .NotEmpty()
                .WithMessage("Email não preenchido.")
                .EmailAddress()
                .WithMessage("Email inválido.");

            RuleFor(entity => entity.Cep)
                .NotEmpty()
                .WithMessage("Cep não preenchido.")
                .Matches(MyRegex.CepFormat)
                .WithMessage("CEP inválido. O formato correto é 00000-000.");

            RuleFor(entity => entity.Cpf)
                .NotEmpty()
                .WithMessage("CPF não preenchido.")
                .Must(FormatExtensions.IsValidCPF)
                .WithMessage("CPF inválido.");

            RuleFor(entity => entity.PhoneNumber)
                .NotEmpty()
                .WithMessage("Telefone não preenchido.")
                .Matches(MyRegex.PhoneFormat)
                .WithMessage("Telefone inválido. O formato correto é +00(00)000000-0000.");
        }
    }
}
