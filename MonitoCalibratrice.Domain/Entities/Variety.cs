using MonitoCalibratrice.Domain.Entities.Interfaces;

namespace MonitoCalibratrice.Domain.Entities
{
    /// <summary>
    /// Rappresenta la varietà di un prodotto (es. “Nadorcot” o “Orri” per i mandarini).
    /// </summary>
    public class Variety : IHasCode
    {
        public Guid Id { get; set; }
        public required string Code { get; set; }
        public required string Name { get; set; }
        public string Description { get; set; } = string.Empty;

        public Guid RawProductId { get; set; }
        public RawProduct RawProduct { get; set; } = default!;
    }
}
