using MonitoCalibratrice.Domain.Entities.Interfaces;

namespace MonitoCalibratrice.Domain.Entities
{
    /// <summary>
    /// Rappresenta l’imballaggio secondario (es. il contenitore del prodotto finito o semi‐lavorato).
    /// </summary>
    public class SecondaryPackaging : IHasCode
    {
        public Guid Id { get; set; }
        public string Code { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Description { get; set; } = string.Empty;
    }
}
