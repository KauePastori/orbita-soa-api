using Orbita.SoaApi.Domain.Enums;

namespace Orbita.SoaApi.Domain.Entities
{
    public class Mission
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid CareerPathId { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int Difficulty { get; set; }
        public int EstimatedMinutes { get; set; }
        public int XpReward { get; set; }
        public MissionStatus Status { get; set; } = MissionStatus.Pendente;
        public CareerPath? CareerPath { get; set; }
        public ICollection<UserMissionProgress> UserProgress { get; set; } = new List<UserMissionProgress>();
    }
}
