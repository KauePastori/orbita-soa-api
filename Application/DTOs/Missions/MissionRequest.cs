namespace Orbita.SoaApi.Application.DTOs.Missions
{
    public class MissionRequest
    {
        public Guid CareerPathId { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int Difficulty { get; set; }
        public int EstimatedMinutes { get; set; }
        public int XpReward { get; set; }
    }
}
