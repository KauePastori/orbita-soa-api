using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Orbita.SoaApi.Application.Common;
using Orbita.SoaApi.Application.DTOs.Missions;
using Orbita.SoaApi.Application.Interfaces;

namespace Orbita.SoaApi.Api.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class MissionsController : ControllerBase
    {
        private readonly IMissionService _service;

        public MissionsController(IMissionService service)
        {
            _service = service;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll([FromQuery] Guid? careerPathId)
        {
            var list = await _service.GetAllAsync(careerPathId);
            return Ok(ApiResponse<List<MissionResponse>>.Ok(list));
        }

        [HttpGet("{id:guid}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(Guid id)
        {
            var m = await _service.GetByIdAsync(id);
            return Ok(ApiResponse<MissionResponse>.Ok(m));
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Mentor")]
        public async Task<IActionResult> Create([FromBody] MissionRequest request)
        {
            var created = await _service.CreateAsync(request);
            return CreatedAtAction(nameof(GetById), new { id = created.Id, version = "1.0" }, ApiResponse<MissionResponse>.Ok(created, "Missão criada com sucesso."));
        }

        [HttpPut("{id:guid}")]
        [Authorize(Roles = "Admin,Mentor")]
        public async Task<IActionResult> Update(Guid id, [FromBody] MissionRequest request)
        {
            var updated = await _service.UpdateAsync(id, request);
            return Ok(ApiResponse<MissionResponse>.Ok(updated, "Missão atualizada com sucesso."));
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
