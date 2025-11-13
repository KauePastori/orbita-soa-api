using Microsoft.AspNetCore.Mvc;
using Orbita.SoaApi.Application.Common;
using Orbita.SoaApi.Application.DTOs.Auth;
using Orbita.SoaApi.Application.Interfaces;

namespace Orbita.SoaApi.Api.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _auth;

        public AuthController(IAuthService auth)
        {
            _auth = auth;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserRequest request)
        {
            var result = await _auth.RegisterAsync(request);
            return Created(string.Empty, ApiResponse<AuthResponse>.Ok(result, "Usuário registrado com sucesso."));
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var result = await _auth.LoginAsync(request);
            return Ok(ApiResponse<AuthResponse>.Ok(result, "Login realizado com sucesso."));
        }
    }
}
