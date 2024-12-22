namespace Uex.ContactBook.Domain.Model.DTOs.Contact
{
    public readonly record struct ContactCreateResponse(
        Guid id,
        string Name,
        string Email,
        string Cpf,
        string PhoneNumber,
        string Address,
        string Cep,
        string GeographicalPosition
    );
}
