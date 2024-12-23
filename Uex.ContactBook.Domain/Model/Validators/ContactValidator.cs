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
                .WithMessage(ContactMessage.NAME_NOT_INFORMED)
                .MinimumLength(10)
                .WithMessage(ContactMessage.NAME_DIGITS);

            RuleFor(entity => entity.Email)
                .NotEmpty()
                .WithMessage(ContactMessage.EMAIL_NOT_INFORMED)
                .EmailAddress()
                .WithMessage(ContactMessage.EMAIL_INVALID);

            RuleFor(entity => entity.Cep)
                .NotEmpty()
                .WithMessage(ContactMessage.CEP_NOT_INFORMED)
                .Matches(MyRegex.CepFormat)
                .WithMessage(ContactMessage.CEP_INVALID);

            RuleFor(entity => entity.Cpf)
                .NotEmpty()
                .WithMessage(ContactMessage.CPF_NOT_INFORMED)
                .Must(FormatExtensions.IsValidCPF)
                .WithMessage(ContactMessage.CPF_INVALID);

            RuleFor(entity => entity.PhoneNumber)
                .NotEmpty()
                .WithMessage(ContactMessage.PHONE_NOT_INFORMED)
                .Matches(MyRegex.PhoneFormat)
                .WithMessage(ContactMessage.PHONE_INVALID);

            RuleFor(entity => entity.City)
                .NotEmpty()
                .WithMessage(ContactMessage.CITY_NOT_INFORMED);

            RuleFor(entity => entity.Uf)
                .NotEmpty()
                .WithMessage(ContactMessage.UF_NOT_INFORMED)
                .Must(IsValidUF)
                .WithMessage(ContactMessage.UF_INVALID);
        }

        private static readonly string[] ValidUFs = new[] { "AC", "AL", "AP", "AM", "BA", "CE", "DF", "ES", "GO", "MA", "MT", "MS", "MG", "PA", "PB", "PR", "PE", "PI", "RJ", "RN", "RS", "RO", "RR", "SC", "SP", "SE", "TO" };
        private bool IsValidUF(string uf) { return Array.Exists(ValidUFs, validUf => validUf.Equals(uf, StringComparison.OrdinalIgnoreCase)); }
    }
}
