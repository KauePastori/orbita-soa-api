using Orbita.SoaApi.Application.DTOs.Progress;

namespace Orbita.SoaApi.Application.Interfaces
{
    public interface IProgressService
    {
        Task<List<ProgressResponse>> GetByUserAsync(Guid userId);
        Task<ProgressResponse> GetByIdAsync(Guid id);
        Task<ProgressResponse> CreateAsync(ProgressRequest request);
        Task<ProgressResponse> UpdateStatusAsync(Guid id, string status);
        Task DeleteAsync(Guid id);
    }
}
