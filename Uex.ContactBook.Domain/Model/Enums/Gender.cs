using System.ComponentModel;

namespace Uex.ContactBook.Domain.Model.Enums
{
    public enum Gender
    {
        [Description("Masculino")]
        Male = 'M',

        [Description("Feminino")]
        Female = 'F',

        [Description("Outros")]
        Others = 'O'
    }
}
