namespace Uex.ContactBook.Domain.Model.Consts
{
    public struct MyRegexConstants
    {
        public const string
            CepFormat = @"^\d{5}-\d{3}$",
            PhoneFormat = @"^\+\d{1,3}\s?\(?\d{2,3}\)?\s?\d{4,5}-?\d{4}$";
    }
}
