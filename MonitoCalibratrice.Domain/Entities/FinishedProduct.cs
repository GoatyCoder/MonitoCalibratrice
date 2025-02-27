using MonitoCalibratrice.Domain.Entities.Interfaces;

namespace MonitoCalibratrice.Domain.Entities
{
    /// <summary>
    /// Rappresenta il prodotto finito (es. cestini di albicocche 10x500g, sacchetti rete di mandarini 12x1kg, etc.).
    /// </summary>
    public class FinishedProduct : IHasCode
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; } = string.Empty;
        public string? Ean { get; set; }
    }
}
