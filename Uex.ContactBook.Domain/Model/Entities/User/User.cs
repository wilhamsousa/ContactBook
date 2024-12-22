using Uex.ContactBook.Domain.Model.Base;
using Uex.ContactBook.Domain.Model.Validators;

namespace Uex.ContactBook.Domain.Model.Entities
{
    public class User : BaseEntity
    {        
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public User() { }

        public User(string userName, string email, string password) 
        {
            UserName = userName;
            Email = email;
            Password = password;
        }

        public void Validate() => Validate(this, new UserValidator());
    }

    public static class UserMessage
    {
        public const string
            USER_USERNAME_ALREADY_EXISTS = "Usuário já cadastrado.",
            USER_EMAIL_ALREADY_EXISTS = "Email já cadastrado.";
    }
}
