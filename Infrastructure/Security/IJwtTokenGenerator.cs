using Orbita.SoaApi.Domain.Entities;

namespace Orbita.SoaApi.Infrastructure.Security
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(User user);
    }
}
