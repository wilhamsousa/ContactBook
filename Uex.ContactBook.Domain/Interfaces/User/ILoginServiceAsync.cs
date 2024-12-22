using Uex.ContactBook.Domain.Model.DTOs.Login;

namespace Uex.ContactBook.Domain.Interfaces
{
    public interface ILoginServiceAsync
    {
        Task<LoginResponse> LoginAsync(LoginRequest param);
    }
}
