using Orbita.SoaApi.Application.DTOs.Auth;

namespace Orbita.SoaApi.Application.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponse> RegisterAsync(RegisterUserRequest request);
        Task<AuthResponse> LoginAsync(LoginRequest request);
    }
}
