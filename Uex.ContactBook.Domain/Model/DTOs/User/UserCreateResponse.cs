namespace Uex.ContactBook.Domain.Model.DTOs.User
{
    public readonly record struct UserCreateResponse(
        string UserName,
        string Email
    );
}
