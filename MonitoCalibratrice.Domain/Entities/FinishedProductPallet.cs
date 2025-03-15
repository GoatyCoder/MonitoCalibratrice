namespace MonitoCalibratrice.Domain.Entities
{
    /// <summary>
    /// Rappresenta una pedana di prodotto finito.
    /// </summary>
    public class FinishedProductPallet
    {
        public Guid Id { get; set; }

        public Guid RawProductId { get; set; }
        public RawProduct RawProduct { get; set; } = default!;

        public Guid VarietyId { get; set; }
        public Variety Variety { get; set; } = default!;

        public string Caliber { get; set; } = string.Empty;

        public Guid FinishedProductId { get; set; }
        public FinishedProduct FinishedProduct { get; set; } = default!;

        public Guid SecondaryPackagingId { get; set; }
        public SecondaryPackaging SecondaryPackaging { get; set; } = default!;

        public Guid? ProductionBatchId { get; set; }
        public ProductionBatch? ProductionBatch { get; set; }

        public string PalletCode { get; set; } = default!;

        public int Units { get; set; }
        public decimal Weight { get; set; }

        public string Annotation { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
