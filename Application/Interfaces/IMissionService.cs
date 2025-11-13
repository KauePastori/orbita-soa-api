using Orbita.SoaApi.Application.DTOs.Missions;

namespace Orbita.SoaApi.Application.Interfaces
{
    public interface IMissionService
    {
        Task<List<MissionResponse>> GetAllAsync(Guid? careerPathId = null);
        Task<MissionResponse> GetByIdAsync(Guid id);
        Task<MissionResponse> CreateAsync(MissionRequest request);
        Task<MissionResponse> UpdateAsync(Guid id, MissionRequest request);
        Task DeleteAsync(Guid id);
    }
}
