using Orbita.SoaApi.Application.DTOs.CareerPaths;

namespace Orbita.SoaApi.Application.Interfaces
{
    public interface ICareerPathService
    {
        Task<List<CareerPathResponse>> GetAllAsync();
        Task<CareerPathResponse> GetByIdAsync(Guid id);
        Task<CareerPathResponse> CreateAsync(CareerPathRequest request);
        Task<CareerPathResponse> UpdateAsync(Guid id, CareerPathRequest request);
        Task DeleteAsync(Guid id);
    }
}
