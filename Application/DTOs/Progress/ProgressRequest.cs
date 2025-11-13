namespace Orbita.SoaApi.Application.DTOs.Progress
{
    public class ProgressRequest
    {
        public Guid UserId { get; set; }
        public Guid MissionId { get; set; }
        public string Status { get; set; } = "EmAndamento";
    }
}
