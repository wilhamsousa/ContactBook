using Uex.ContactBook.Domain.Model.Base;
using Uex.ContactBook.Domain.Model.Validators;

namespace Uex.ContactBook.Domain.Model.Entities
{
    public class Contact : BaseEntity
    {
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Cpf { get; set; }
        public string PhoneNumber { get; set; }
        public string Cep { get; set; }
        public string Address { get; set; }
        public string Complement { get; set; }
        public string City { get; set; }
        public string Uf { get; set; }
        public string GeographicalPosition { get; set; }

        public virtual User User { get; set; }

        public Contact() 
        {
            
        }

        public void Validate() => Validate(this, new ContactValidator());
    }

    public static class ContactMessage
    {
        public const string
            ID_IS_EMPTY = "Id não informado.",
            CPF_ALREADY_EXISTS = "CPF já cadastrado.",
            NAME_ALREADY_EXISTS = "Nome já cadastrado.",
            EMAIL_ALREADY_EXISTS = "Email já cadastrado.",
            CONTACT_NOTFOUND = "Contato não encontrado.",
            NAME_DIGITS = "Nome deve conter pelo menos 10 dígitos.",
            NAME_NOT_INFORMED = "Nome não informado.",
            EMAIL_NOT_INFORMED = "Email não informado.",
            CPF_NOT_INFORMED = "CPF não informado.",
            PHONE_NOT_INFORMED = "Telefone não informado.",
            CEP_NOT_INFORMED = "CEP não informado.",
            CITY_NOT_INFORMED = "Cidade não informada.",
            UF_NOT_INFORMED = "UF não informada.",
            EMAIL_INVALID = "Email inválido.",
            PHONE_INVALID = "Telefone inválido. O formato correto é +00(00)000000-0000.",
            CPF_INVALID = "Cpf inválido.",
            CEP_INVALID = "CEP inválido. O formato correto é 00000-000.",
            UF_INVALID = "UF inválida.";
    }
}
