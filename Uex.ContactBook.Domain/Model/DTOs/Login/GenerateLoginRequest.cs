using System.Security.Claims;

namespace Uex.ContactBook.Domain.Model.DTOs.Login
{
    public readonly record struct GenerateLoginRequest(
        IEnumerable<Claim> claims,
        DateTime expires,
        string issuer,
        string audiance,
        string base64Secret
    );
}
