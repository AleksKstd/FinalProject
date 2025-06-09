using FinalProject.Services.DTOs.Authentication;

namespace FinalProject.Services.Interfaces.Authentication
{
    public interface IAuthenticationService
    {
        Task<LoginResponse> LoginAsync(LoginRequest request);
    }
}
