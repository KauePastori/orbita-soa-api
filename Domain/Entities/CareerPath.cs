namespace Orbita.SoaApi.Domain.Entities
{
    public class CareerPath
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = null!;
        public string Area { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Level { get; set; } = "Iniciante";
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public ICollection<Mission> Missions { get; set; } = new List<Mission>();
    }
}
