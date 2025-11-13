using Orbita.SoaApi.Domain.Enums;

namespace Orbita.SoaApi.Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public UserRole Role { get; set; } = UserRole.Student;
        public int WeeklyAvailableHours { get; set; }
        public ICollection<UserMissionProgress> Progress { get; set; } = new List<UserMissionProgress>();
    }
}
