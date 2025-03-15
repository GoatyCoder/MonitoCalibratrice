namespace MonitoCalibratrice.Models
{
    public class FinishedProductPalletModel
    {
        public Guid Id { get; set; }

        public Guid RawProductId { get; set; }

        public Guid VarietyId { get; set; }

        public string Caliber { get; set; }

        public Guid FinishedProductId { get; set; }

        public Guid SecondaryPackagingId { get; set; }

        public Guid? ProductionBatchId { get; set; }

        public string PalletCode { get; set; }

        public int Units { get; set; }
        public decimal Weight { get; set; }

        public string Annotation { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
