using Orbita.SoaApi.Domain.Enums;

namespace Orbita.SoaApi.Domain.Entities
{
    public class UserMissionProgress
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid UserId { get; set; }
        public Guid MissionId { get; set; }
        public MissionStatus Status { get; set; } = MissionStatus.Pendente;
        public DateTime StartedAt { get; set; } = DateTime.UtcNow;
        public DateTime? CompletedAt { get; set; }
        public User? User { get; set; }
        public Mission? Mission { get; set; }
    }
}
