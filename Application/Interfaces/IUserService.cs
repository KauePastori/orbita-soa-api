using Orbita.SoaApi.Application.DTOs.Users;

namespace Orbita.SoaApi.Application.Interfaces
{
    public interface IUserService
    {
        Task<List<UserResponse>> GetAllAsync();
        Task<UserResponse> GetByIdAsync(Guid id);
        Task<UserResponse> GetCurrentAsync(Guid id);
        Task<UserResponse> UpdateRoleAsync(Guid id, UpdateUserRoleRequest request);
    }
}
