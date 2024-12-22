namespace Uex.ContactBook.Domain.Model.DTOs.User
{
    public readonly record struct UserLoginResponse(
        string UserName,
        string Email,
        string AccessToken,
        DateTime CreatedUtcDateTime,
        string RefreshToken,
        DateTime ExpiresOnAccessToken,
        DateTime ExpiresOnRefreshToken
    );
}
