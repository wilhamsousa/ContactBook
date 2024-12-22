namespace Uex.ContactBook.Domain.Model.DTOs.User
{
    public readonly record struct UserLoginRequest(
        string UserName,
        string Email,
        string Password
    );
}
