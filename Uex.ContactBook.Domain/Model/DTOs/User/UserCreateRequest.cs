namespace Uex.ContactBook.Domain.Model.DTOs.User
{
    public readonly record struct UserCreateRequest(
        string UserName,
        string Email,
        string Cpf,
        string PhoneNumber,
        string Address,
        string Cep,
        string GeographicalPosition
    );
}
