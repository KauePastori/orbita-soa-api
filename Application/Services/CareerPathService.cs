using Microsoft.EntityFrameworkCore;
using Orbita.SoaApi.Application.DTOs.CareerPaths;
using Orbita.SoaApi.Application.Interfaces;
using Orbita.SoaApi.Domain.Entities;
using Orbita.SoaApi.Domain.Exceptions;
using Orbita.SoaApi.Infrastructure.Data;

namespace Orbita.SoaApi.Application.Services
{
    public class CareerPathService : ICareerPathService
    {
        private readonly OrbitaContext _context;

        public CareerPathService(OrbitaContext context)
        {
            _context = context;
        }

        public async Task<List<CareerPathResponse>> GetAllAsync()
        {
            var list = await _context.CareerPaths.AsNoTracking().ToListAsync();
            return list.Select(ToDto).ToList();
        }

        public async Task<CareerPathResponse> GetByIdAsync(Guid id)
        {
            var cp = await _context.CareerPaths.FindAsync(id);
            if (cp == null) throw new NotFoundException("Rota de carreira não encontrada.");
            return ToDto(cp);
        }

        public async Task<CareerPathResponse> CreateAsync(CareerPathRequest request)
        {
            var entity = new CareerPath
            {
                Name = request.Name,
                Area = request.Area,
                Description = request.Description,
                Level = request.Level
            };
            _context.CareerPaths.Add(entity);
            await _context.SaveChangesAsync();
            return ToDto(entity);
        }

        public async Task<CareerPathResponse> UpdateAsync(Guid id, CareerPathRequest request)
        {
            var entity = await _context.CareerPaths.FindAsync(id);
            if (entity == null) throw new NotFoundException("Rota de carreira não encontrada.");

            entity.Name = request.Name;
            entity.Area = request.Area;
            entity.Description = request.Description;
            entity.Level = request.Level;
            await _context.SaveChangesAsync();
            return ToDto(entity);
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await _context.CareerPaths.Include(c => c.Missions).FirstOrDefaultAsync(c => c.Id == id);
            if (entity == null) throw new NotFoundException("Rota de carreira não encontrada.");
            if (entity.Missions.Any())
                throw new ValidationException("Não é possível excluir rota com missões associadas.");
            _context.CareerPaths.Remove(entity);
            await _context.SaveChangesAsync();
        }

        private static CareerPathResponse ToDto(CareerPath c) => new CareerPathResponse
        {
            Id = c.Id,
            Name = c.Name,
            Area = c.Area,
            Description = c.Description,
            Level = c.Level
        };
    }
}
