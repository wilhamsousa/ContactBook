using Uex.ContactBook.Domain.Model.Base;
using Uex.ContactBook.Domain.Model.Enums;
using Uex.ContactBook.Domain.Model.Validators;

namespace Uex.ContactBook.Domain.Model.Entities
{
    public class User : BaseEntity
    {        
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Cpf { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Cep { get; set; }
        public string GeographicalPosition { get; set; }

        public User() { }

        //public User(string userName)
        //{
        //    UserName = userName;
        //    Validate(this, new UserValidator());
        //}
    }

    public static class UserMessage
    {
        public const string
            USER_USERNAME_ALREADY_EXISTS = "Usuário já cadastrado.";
    }
}
