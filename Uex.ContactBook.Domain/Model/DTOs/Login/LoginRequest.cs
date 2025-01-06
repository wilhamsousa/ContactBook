namespace Uex.ContactBook.Domain.Model.DTOs.Login
{
    public readonly record struct LoginRequest(
        string Email,
        string Password
    );
}
