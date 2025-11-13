namespace Orbita.SoaApi.Application.DTOs.Missions
{
    public class MissionResponse
    {
        public Guid Id { get; set; }
        public Guid CareerPathId { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int Difficulty { get; set; }
        public int EstimatedMinutes { get; set; }
        public int XpReward { get; set; }
        public string Status { get; set; } = null!;
    }
}
