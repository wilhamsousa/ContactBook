using System.Security.Claims;

namespace Uex.ContactBook.Domain.Model.DTOs.User
{
    public readonly record struct GenerateRequest(
        IEnumerable<Claim> claims,
        DateTime expires,
        string issuer,
        string audiance,
        string base64Secret
    );
}
