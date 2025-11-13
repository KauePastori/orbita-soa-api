using Microsoft.AspNetCore.Mvc;

namespace Orbita.SoaApi.Api.Controllers
{
    /// <summary>
    /// Versão 2 da API de rotas de carreira.
    /// Usada para demonstrar versionamento (/api/v2/...).
    /// </summary>
    [ApiController]
    [ApiVersion("2.0")]
    [ApiExplorerSettings(IgnoreApi = true)] // <-- Swagger vai ignorar este controller
    [Route("api/v{version:apiVersion}/CareerPaths")]
    public class CareerPathsV2Controller : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new
            {
                version = "2.0",
                message = "Exemplo de versão v2 da ORBITA SOA API para demonstrar versionamento de WebServices."
            });
        }
    }
}
