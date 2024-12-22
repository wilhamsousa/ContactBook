namespace Uex.ContactBook.Domain.Model.DTOs.Login
{
    public readonly record struct LoginResponse(
        string UserName,
        string Email,
        string AccessToken,
        DateTime CreatedUtcDateTime,
        string RefreshToken,
        DateTime ExpiresOnAccessToken,
        DateTime ExpiresOnRefreshToken
    );
}
