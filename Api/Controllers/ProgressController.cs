using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Orbita.SoaApi.Application.Common;
using Orbita.SoaApi.Application.DTOs.Progress;
using Orbita.SoaApi.Application.Interfaces;

namespace Orbita.SoaApi.Api.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize]
    public class ProgressController : ControllerBase
    {
        private readonly IProgressService _service;

        public ProgressController(IProgressService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetForCurrentUser()
        {
            var id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (id == null) return Unauthorized(ApiResponse<string>.Fail("Usuário não autenticado."));
            var list = await _service.GetByUserAsync(Guid.Parse(id));
            return Ok(ApiResponse<List<ProgressResponse>>.Ok(list));
        }

        [HttpGet("{id:guid}")]
        [Authorize(Roles = "Admin,Mentor")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var p = await _service.GetByIdAsync(id);
            return Ok(ApiResponse<ProgressResponse>.Ok(p));
        }

        [HttpPost]
        [Authorize(Roles = "Student")]
        public async Task<IActionResult> Create([FromBody] ProgressRequest request)
        {
            var id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (id == null) return Unauthorized(ApiResponse<string>.Fail("Usuário não autenticado."));
            request.UserId = Guid.Parse(id);
            var created = await _service.CreateAsync(request);
            return CreatedAtAction(nameof(GetById), new { id = created.Id, version = "1.0" }, ApiResponse<ProgressResponse>.Ok(created, "Progresso criado com sucesso."));
        }

        [HttpPut("{id:guid}/status")]
        [Authorize(Roles = "Student")]
        public async Task<IActionResult> UpdateStatus(Guid id, [FromBody] string status)
        {
            var updated = await _service.UpdateStatusAsync(id, status);
            return Ok(ApiResponse<ProgressResponse>.Ok(updated, "Status atualizado com sucesso."));
        }

        [HttpDelete("{id:guid}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}
