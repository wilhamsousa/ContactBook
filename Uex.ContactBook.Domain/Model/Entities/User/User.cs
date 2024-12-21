using Uex.ContactBook.Domain.Model.Base;
using Uex.ContactBook.Domain.Model.Validators;

namespace Uex.ContactBook.Domain.Model.Entities
{
    public class User : BaseEntity
    {        
        public string UserName { get; set; }
        public string Email { get; set; }

        public User() { }

        public User(string userName, string email) 
        {
            UserName = userName;
            Email = email;
        }

        public void Validate() => Validate(this, new UserValidator());
    }

    public static class UserMessage
    {
        public const string
            USER_USERNAME_ALREADY_EXISTS = "Usuário já cadastrado.";
    }
}
