using MonitoCalibratrice.Domain.Entities.Interfaces;

namespace MonitoCalibratrice.Domain.Entities
{
    public class Variety : IHasCode
    {
        public Guid Id { get; set; }
        public required string Code { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }

        public Guid RawProductId { get; set; }
        public RawProduct RawProduct { get; set; } = default!;
    }
}
