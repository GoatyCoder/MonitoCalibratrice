namespace MonitoCalibratrice.Domain.Entities
{
    /// <summary>
    /// Rappresenta una lavorazione (processo produttivo) che può essere in corso o conclusa.
    /// </summary>
    public class ProductionBatch
    {
        public Guid Id { get; set; }

        public Guid RawProductId { get; set; }
        public RawProduct RawProduct { get; set; } = default!;

        public Guid VarietyId { get; set; }
        public Variety Variety { get; set; } = default!;

        public string? Caliber { get; set; }

        public Guid FinishedProductId { get; set; }
        public FinishedProduct FinishedProduct { get; set; } = default!;

        public Guid SecondaryPackagingId { get; set; }
        public SecondaryPackaging SecondaryPackaging { get; set; } = default!;

        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }

        public ICollection<FinishedProductPallet> Pallets { get; set; } = new List<FinishedProductPallet>();

        public bool IsCompleted => EndTime.HasValue;
    }
}
