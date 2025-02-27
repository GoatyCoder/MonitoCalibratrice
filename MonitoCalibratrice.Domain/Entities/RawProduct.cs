using MonitoCalibratrice.Domain.Entities.Interfaces;

namespace MonitoCalibratrice.Domain.Entities
{
    public class RawProduct : IHasCode
    {
        public Guid Id { get; set; }
        public required string Code { get; set; }
        public required string Name { get; set; }
        public string Description { get; set; } = string.Empty;

        public ICollection<Variety> Varieties { get; set; } = new List<Variety>();
    }
}
