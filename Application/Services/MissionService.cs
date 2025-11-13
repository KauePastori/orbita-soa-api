using Microsoft.EntityFrameworkCore;
using Orbita.SoaApi.Application.DTOs.Missions;
using Orbita.SoaApi.Application.Interfaces;
using Orbita.SoaApi.Domain.Entities;
using Orbita.SoaApi.Domain.Enums;
using Orbita.SoaApi.Domain.Exceptions;
using Orbita.SoaApi.Infrastructure.Data;

namespace Orbita.SoaApi.Application.Services
{
    public class MissionService : IMissionService
    {
        private readonly OrbitaContext _context;

        public MissionService(OrbitaContext context)
        {
            _context = context;
        }

        public async Task<List<MissionResponse>> GetAllAsync(Guid? careerPathId = null)
        {
            var q = _context.Missions.AsNoTracking().AsQueryable();
            if (careerPathId.HasValue)
                q = q.Where(m => m.CareerPathId == careerPathId.Value);
            var list = await q.ToListAsync();
            return list.Select(ToDto).ToList();
        }

        public async Task<MissionResponse> GetByIdAsync(Guid id)
        {
            var m = await _context.Missions.FindAsync(id);
            if (m == null) throw new NotFoundException("Missão não encontrada.");
            return ToDto(m);
        }

        public async Task<MissionResponse> CreateAsync(MissionRequest request)
        {
            var career = await _context.CareerPaths.FindAsync(request.CareerPathId);
            if (career == null) throw new ValidationException("Rota de carreira inválida.");

            var entity = new Mission
            {
                CareerPathId = request.CareerPathId,
                Title = request.Title,
                Description = request.Description,
                Difficulty = request.Difficulty,
                EstimatedMinutes = request.EstimatedMinutes,
                XpReward = request.XpReward,
                Status = MissionStatus.Pendente
            };
            _context.Missions.Add(entity);
            await _context.SaveChangesAsync();
            return ToDto(entity);
        }

        public async Task<MissionResponse> UpdateAsync(Guid id, MissionRequest request)
        {
            var m = await _context.Missions.FindAsync(id);
            if (m == null) throw new NotFoundException("Missão não encontrada.");
            m.Title = request.Title;
            m.Description = request.Description;
            m.Difficulty = request.Difficulty;
            m.EstimatedMinutes = request.EstimatedMinutes;
            m.XpReward = request.XpReward;
            await _context.SaveChangesAsync();
            return ToDto(m);
        }

        public async Task DeleteAsync(Guid id)
        {
            var m = await _context.Missions.Include(x => x.UserProgress).FirstOrDefaultAsync(x => x.Id == id);
            if (m == null) throw new NotFoundException("Missão não encontrada.");
            if (m.UserProgress.Any())
                throw new ValidationException("Não é possível excluir missão com progresso registrado.");
            _context.Missions.Remove(m);
            await _context.SaveChangesAsync();
        }

        private static MissionResponse ToDto(Mission m) => new MissionResponse
        {
            Id = m.Id,
            CareerPathId = m.CareerPathId,
            Title = m.Title,
            Description = m.Description,
            Difficulty = m.Difficulty,
            EstimatedMinutes = m.EstimatedMinutes,
            XpReward = m.XpReward,
            Status = m.Status.ToString()
        };
    }
}
