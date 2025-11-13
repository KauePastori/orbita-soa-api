namespace Orbita.SoaApi.Application.DTOs.Progress
{
    public class ProgressResponse
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid MissionId { get; set; }
        public string Status { get; set; } = null!;
        public DateTime StartedAt { get; set; }
        public DateTime? CompletedAt { get; set; }
    }
}
