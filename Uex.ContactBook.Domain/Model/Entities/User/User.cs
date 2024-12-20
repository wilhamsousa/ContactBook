using Uex.ContactBook.Domain.Model.Base;
using Uex.ContactBook.Domain.Model.Enums;
using Uex.ContactBook.Domain.Model.Validators;

namespace Uex.ContactBook.Domain.Model.Entities
{
    public class User : BaseEntity
    {        
        public string UserName { get; set; }

        public User() { }

        public User(string userName)
        {
            UserName = userName;
            Validate(this, new UserValidator());
        }
    }

    public static class UserMessage
    {
        public const string
            USER_USERNAME_ALREADY_EXISTS = "Usuário já cadastrado.";
    }
}
