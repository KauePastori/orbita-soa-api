namespace Orbita.SoaApi.Application.DTOs.Users
{
    public class UserResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Role { get; set; } = null!;
        public int WeeklyAvailableHours { get; set; }
    }
}
