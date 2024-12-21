namespace Uex.ContactBook.Domain.Model.DTOs.User
{
    public readonly record struct UserGetAllResponse(
        string UserName,
        string Email
    );
}
