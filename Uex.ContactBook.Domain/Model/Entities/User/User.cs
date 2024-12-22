using Uex.ContactBook.Domain.Model.Base;
using Uex.ContactBook.Domain.Model.Validators;

namespace Uex.ContactBook.Domain.Model.Entities
{
    public class User : BaseEntity
    {        
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public virtual ICollection<Contact>? ContactList { get; set; }

        public User() 
        {
            ContactList = new HashSet<Contact>();
        }

        public User(string userName, string email, string password) 
        {
            ContactList = new HashSet<Contact>();

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
            USER_EMAIL_ALREADY_EXISTS = "Email já cadastrado.",
            USERNAME_NOT_INFORMED = "Usuário não preenchido.",
            USER_NOTFOUND = "Usuário não encontrado.",
            USERNAME_DIGITS = "Usuário deve conter pelo menos 10 dígitos.",
            EMAIL_NOT_INFORMED = "Email não informado.",
            EMAIL_INVALID = "Email inválido.",
            PASSWORD_NOT_INFORMED = "Senha não informada.",
            PASSWORD_DIGITS = "Senha deve conter pelo menos 6 dígitos.";
    }
}
