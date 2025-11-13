using Microsoft.EntityFrameworkCore;
using Orbita.SoaApi.Application.DTOs.Users;
using Orbita.SoaApi.Application.Interfaces;
using Orbita.SoaApi.Domain.Enums;
using Orbita.SoaApi.Domain.Exceptions;
using Orbita.SoaApi.Infrastructure.Data;

namespace Orbita.SoaApi.Application.Services
{
    public class UserService : IUserService
    {
        private readonly OrbitaContext _context;

        public UserService(OrbitaContext context)
        {
            _context = context;
        }

        public async Task<List<UserResponse>> GetAllAsync()
        {
            var users = await _context.Users.AsNoTracking().ToListAsync();
            return users.Select(ToDto).ToList();
        }

        public async Task<UserResponse> GetByIdAsync(Guid id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) throw new NotFoundException("Usuário não encontrado.");
            return ToDto(user);
        }

        public async Task<UserResponse> GetCurrentAsync(Guid id)
        {
            return await GetByIdAsync(id);
        }

        public async Task<UserResponse> UpdateRoleAsync(Guid id, UpdateUserRoleRequest request)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) throw new NotFoundException("Usuário não encontrado.");

            if (!Enum.TryParse<UserRole>(request.Role, true, out var newRole))
                throw new ValidationException("Papel inválido.");

            user.Role = newRole;
            await _context.SaveChangesAsync();
            return ToDto(user);
        }

        private static UserResponse ToDto(Domain.Entities.User u) => new UserResponse
        {
            Id = u.Id,
            Name = u.Name,
            Email = u.Email,
            Role = u.Role.ToString(),
            WeeklyAvailableHours = u.WeeklyAvailableHours
        };
    }
}
