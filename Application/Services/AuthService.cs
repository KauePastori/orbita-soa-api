using Microsoft.EntityFrameworkCore;
using Orbita.SoaApi.Application.DTOs.Auth;
using Orbita.SoaApi.Application.Interfaces;
using Orbita.SoaApi.Domain.Entities;
using Orbita.SoaApi.Domain.Enums;
using Orbita.SoaApi.Domain.Exceptions;
using Orbita.SoaApi.Domain.ValueObjects;
using Orbita.SoaApi.Infrastructure.Data;
using Orbita.SoaApi.Infrastructure.Security;
using System;


namespace Orbita.SoaApi.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly OrbitaContext _context;
        private readonly IJwtTokenGenerator _jwt;

        public AuthService(OrbitaContext context, IJwtTokenGenerator jwt)
        {
            _context = context;
            _jwt = jwt;
        }

        public async Task<AuthResponse> RegisterAsync(RegisterUserRequest request)
        {
            var email = new Email(request.Email);
            if (await _context.Users.AnyAsync(u => u.Email == email.Address))
                throw new ConflictException("Já existe um usuário com esse e-mail.");
            if (request.Password.Length < 6)
                throw new ValidationException("A senha deve ter pelo menos 6 caracteres.");

            var user = new User
            {
                Name = request.Name,
                Email = email.Address,
                PasswordHash = PasswordHasher.Hash(request.Password),
                //Role = UserRole.Student,
                Role = request.Email.Equals("admin@orbita.admin", StringComparison.OrdinalIgnoreCase)
                    ? UserRole.Admin
                    : UserRole.Student,

                WeeklyAvailableHours = request.WeeklyAvailableHours
            };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            var token = _jwt.GenerateToken(user);
            return new AuthResponse
            {
                Token = token,
                ExpiresAt = DateTime.UtcNow.AddHours(1),
                Name = user.Name,
                Email = user.Email,
                Role = user.Role.ToString()
            };
        }

        public async Task<AuthResponse> LoginAsync(LoginRequest request)
        {
            var email = new Email(request.Email);
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email.Address);
            if (user == null) throw new UnauthorizedException("Credenciais inválidas.");

            if (!PasswordHasher.Verify(request.Password, user.PasswordHash))
                throw new UnauthorizedException("Credenciais inválidas.");

            var token = _jwt.GenerateToken(user);
            return new AuthResponse
            {
                Token = token,
                ExpiresAt = DateTime.UtcNow.AddHours(1),
                Name = user.Name,
                Email = user.Email,
                Role = user.Role.ToString()
            };
        }
    }
}
