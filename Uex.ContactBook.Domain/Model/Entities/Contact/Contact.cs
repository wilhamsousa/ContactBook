using Uex.ContactBook.Domain.Model.Base;
using Uex.ContactBook.Domain.Model.Validators;

namespace Uex.ContactBook.Domain.Model.Entities
{
    public class Contact : BaseEntity
    {        
        public string Name { get; set; }
        public string Email { get; set; }
        public string Cpf { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Cep { get; set; }
        public string GeographicalPosition { get; set; }

        public Contact() 
        {
            
        }

        public void Validate() => Validate(this, new ContactValidator());
    }

    public static class ContactMessage
    {
        public const string
            CONTACT_CPF_ALREADY_EXISTS = "CPF já cadastrado.";
    }
}
