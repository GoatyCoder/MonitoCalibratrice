using MonitoCalibratrice.Domain.Entities.Interfaces;

namespace MonitoCalibratrice.Domain.Entities
{
    public class SecondaryPackaging : IHasCode
    {
        public Guid Id { get; set; }
        public required string Code { get; set; } = null!;
        public required string Name { get; set; } = null!;
        public string? Description { get; set; }
    }
}
