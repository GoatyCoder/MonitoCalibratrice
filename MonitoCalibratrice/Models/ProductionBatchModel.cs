namespace MonitoCalibratrice.Models
{
    public class ProductionBatchModel
    {
        public Guid Id { get; set; }
        public Guid ProductionLineId { get; set; }
        public string ProductionLineName { get; set; } = string.Empty;
        public Guid RawProductId { get; set; }
        public string RawProductName { get; set; } = string.Empty;
        public Guid VarietyId { get; set; }
        public string VarietyName { get; set; } = string.Empty;
        public string? Caliber { get; set; }
        public Guid FinishedProductId { get; set; }
        public string FinishedProductName { get; set; } = string.Empty;
        public Guid SecondaryPackagingId { get; set; }
        public string SecondaryPackagingName { get; set; } = string.Empty;
        public int TotalUnits { get; set; }
        public DateTime StartedAt { get; set; }
        public DateTime? FinishedAt { get; set; }
        public ICollection<FinishedProductPalletModel> Pallets { get; set; } = new List<FinishedProductPalletModel>();
        public bool IsCompleted => FinishedAt.HasValue;
    }
}
