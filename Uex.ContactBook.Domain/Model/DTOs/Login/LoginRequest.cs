namespace Uex.ContactBook.Domain.Model.DTOs.Login
{
    public readonly record struct LoginRequest(
        string UserName,
        string Email,
        string Password
    );
}
