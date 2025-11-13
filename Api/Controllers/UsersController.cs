using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Orbita.SoaApi.Application.Common;
using Orbita.SoaApi.Application.DTOs.Users;
using Orbita.SoaApi.Application.Interfaces;

namespace Orbita.SoaApi.Api.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _users;

        public UsersController(IUserService users)
        {
            _users = users;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll()
        {
            var list = await _users.GetAllAsync();
            return Ok(ApiResponse<List<UserResponse>>.Ok(list));
        }

        [HttpGet("{id:guid}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var user = await _users.GetByIdAsync(id);
            return Ok(ApiResponse<UserResponse>.Ok(user));
        }

        [HttpGet("me")]
        public async Task<IActionResult> GetMe()
        {
            var id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (id == null) return Unauthorized(ApiResponse<string>.Fail("Usuário não autenticado."));
            var user = await _users.GetCurrentAsync(Guid.Parse(id));
            return Ok(ApiResponse<UserResponse>.Ok(user));
        }

        [HttpPut("{id:guid}/role")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateRole(Guid id, [FromBody] UpdateUserRoleRequest request)
        {
            var updated = await _users.UpdateRoleAsync(id, request);
            return Ok(ApiResponse<UserResponse>.Ok(updated, "Papel atualizado com sucesso."));
        }
    }
}
