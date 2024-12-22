using System.Security.Claims;
using Uex.ContactBook.Domain.Model.DTOs.User;

namespace Uex.ContactBook.Domain.Interfaces
{
    public interface ITokenGeneratorService
    {
        string Generate(GenerateRequest param);
    }
}
