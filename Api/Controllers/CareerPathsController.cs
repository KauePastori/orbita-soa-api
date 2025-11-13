using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Orbita.SoaApi.Application.Common;
using Orbita.SoaApi.Application.DTOs.CareerPaths;
using Orbita.SoaApi.Application.Interfaces;

namespace Orbita.SoaApi.Api.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class CareerPathsController : ControllerBase
    {
        private readonly ICareerPathService _service;

        public CareerPathsController(ICareerPathService service)
        {
            _service = service;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll()
        {
            var list = await _service.GetAllAsync();
            return Ok(ApiResponse<List<CareerPathResponse>>.Ok(list));
        }

        [HttpGet("{id:guid}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(Guid id)
        {
            var cp = await _service.GetByIdAsync(id);
            return Ok(ApiResponse<CareerPathResponse>.Ok(cp));
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Mentor")]
        public async Task<IActionResult> Create([FromBody] CareerPathRequest request)
        {
            var created = await _service.CreateAsync(request);
            return CreatedAtAction(nameof(GetById), new { id = created.Id, version = "1.0" }, ApiResponse<CareerPathResponse>.Ok(created, "Rota criada com sucesso."));
        }

        [HttpPut("{id:guid}")]
        [Authorize(Roles = "Admin,Mentor")]
        public async Task<IActionResult> Update(Guid id, [FromBody] CareerPathRequest request)
        {
            var updated = await _service.UpdateAsync(id, request);
            return Ok(ApiResponse<CareerPathResponse>.Ok(updated, "Rota atualizada com sucesso."));
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
