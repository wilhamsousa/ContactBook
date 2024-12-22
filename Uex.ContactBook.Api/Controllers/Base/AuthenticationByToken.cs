namespace Uex.ContactBook.Api.Controllers.Base
{
    public readonly record struct AuthenticationByToken (
        Guid UserId,
        string UserEmail,
        string AccessToken
    );
}
