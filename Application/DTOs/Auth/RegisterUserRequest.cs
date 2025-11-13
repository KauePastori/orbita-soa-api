namespace Orbita.SoaApi.Application.DTOs.Auth
{
    public class RegisterUserRequest
    {
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public int WeeklyAvailableHours { get; set; }
    }
}
