namespace MonitoCalibratrice.Domain.Entities
{
    public class ProductionBatch
    {
        public Guid Id { get; set; }

        public Guid ProductionLineId { get; set; }
        public ProductionLine ProductionLine { get; set; } = default!;

        public Guid RawProductId { get; set; }
        public RawProduct RawProduct { get; set; } = default!;

        public Guid VarietyId { get; set; }
        public Variety Variety { get; set; } = default!;

        public string? Caliber { get; set; }

        public Guid FinishedProductId { get; set; }
        public FinishedProduct FinishedProduct { get; set; } = default!;

        public Guid SecondaryPackagingId { get; set; }
        public SecondaryPackaging SecondaryPackaging { get; set; } = default!;

        public ICollection<FinishedProductPallet> Pallets { get; set; } = new List<FinishedProductPallet>();

        public DateTime StartedAt { get; set; }
        public DateTime? FinishedAt { get; set; }       

        public bool IsCompleted => FinishedAt.HasValue;
    }
}
