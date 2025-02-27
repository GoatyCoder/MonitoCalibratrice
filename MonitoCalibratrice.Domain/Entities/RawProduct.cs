using MonitoCalibratrice.Domain.Entities.Interfaces;

namespace MonitoCalibratrice.Domain.Entities
{
    /// <summary>
    /// Rappresenta un prodotto agricolo da trasformare (es. mandarini, albicocche, pesche, etc.)
    /// </summary>
    public class RawProduct : IHasCode
    {
        public Guid Id { get; set; }
        public required string Code { get; set; }
        public required string Name { get; set; }
        public string Description { get; set; } = string.Empty;

        public ICollection<Variety> Varieties { get; set; } = new List<Variety>();
    }
}
