

using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Uex.ContactBook.Domain.Interfaces;
using Uex.ContactBook.Domain.Model.DTOs.User;

namespace Uex.ContactBook.Application.Services
{
    public class TokenGeneratorService : ITokenGeneratorService
    {
        public string Generate(GenerateRequest param)
        {
            var identityClaims = new ClaimsIdentity(param.claims);
            var tokenHandler = new JwtSecurityTokenHandler();

            var symmetricKey = Convert.FromBase64String(param.base64Secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = identityClaims,
                Issuer = param.issuer,
                Audience = param.audiance,
                Expires = param.expires,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(symmetricKey), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
