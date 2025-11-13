using Microsoft.EntityFrameworkCore;
using Orbita.SoaApi.Application.DTOs.Progress;
using Orbita.SoaApi.Application.Interfaces;
using Orbita.SoaApi.Domain.Entities;
using Orbita.SoaApi.Domain.Enums;
using Orbita.SoaApi.Domain.Exceptions;
using Orbita.SoaApi.Infrastructure.Data;

namespace Orbita.SoaApi.Application.Services
{
    public class ProgressService : IProgressService
    {
        private readonly OrbitaContext _context;

        public ProgressService(OrbitaContext context)
        {
            _context = context;
        }

        public async Task<List<ProgressResponse>> GetByUserAsync(Guid userId)
        {
            var list = await _context.UserMissionProgresses.AsNoTracking().Where(p => p.UserId == userId).ToListAsync();
            return list.Select(ToDto).ToList();
        }

        public async Task<ProgressResponse> GetByIdAsync(Guid id)
        {
            var p = await _context.UserMissionProgresses.FindAsync(id);
            if (p == null) throw new NotFoundException("Progresso não encontrado.");
            return ToDto(p);
        }

        public async Task<ProgressResponse> CreateAsync(ProgressRequest request)
        {
            var user = await _context.Users.FindAsync(request.UserId);
            if (user == null) throw new ValidationException("Usuário inválido.");
            var mission = await _context.Missions.FindAsync(request.MissionId);
            if (mission == null) throw new ValidationException("Missão inválida.");

            if (!Enum.TryParse<MissionStatus>(request.Status, true, out var status))
                status = MissionStatus.EmAndamento;

            var p = new UserMissionProgress
            {
                UserId = request.UserId,
                MissionId = request.MissionId,
                Status = status,
                StartedAt = DateTime.UtcNow
            };
            _context.UserMissionProgresses.Add(p);
            await _context.SaveChangesAsync();
            return ToDto(p);
        }

        public async Task<ProgressResponse> UpdateStatusAsync(Guid id, string status)
        {
            var p = await _context.UserMissionProgresses.FindAsync(id);
            if (p == null) throw new NotFoundException("Progresso não encontrado.");
            if (!Enum.TryParse<MissionStatus>(status, true, out var newStatus))
                throw new ValidationException("Status inválido.");
            p.Status = newStatus;
            if (newStatus == MissionStatus.Concluida)
                p.CompletedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return ToDto(p);
        }

        public async Task DeleteAsync(Guid id)
        {
            var p = await _context.UserMissionProgresses.FindAsync(id);
            if (p == null) throw new NotFoundException("Progresso não encontrado.");
            _context.UserMissionProgresses.Remove(p);
            await _context.SaveChangesAsync();
        }

        private static ProgressResponse ToDto(UserMissionProgress p) => new ProgressResponse
        {
            Id = p.Id,
            UserId = p.UserId,
            MissionId = p.MissionId,
            Status = p.Status.ToString(),
            StartedAt = p.StartedAt,
            CompletedAt = p.CompletedAt
        };
    }
}
