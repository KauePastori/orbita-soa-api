namespace Orbita.SoaApi.Application.DTOs.CareerPaths
{
    public class CareerPathResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Area { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Level { get; set; } = null!;
    }
}
