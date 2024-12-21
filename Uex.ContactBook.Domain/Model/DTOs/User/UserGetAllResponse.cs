namespace Uex.ContactBook.Domain.Model.DTOs.User
{
    public readonly record struct UserGetAllResponse(
        string UserName,
        string Email,
        string Cpf,
        string PhoneNumber,
        string Address,
        string Cep,
        string GeographicalPosition
    );
}
