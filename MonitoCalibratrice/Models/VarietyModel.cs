namespace MonitoCalibratrice.Models
{
    public class VarietyModel
    {
        public Guid Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public Guid RawProductId { get; set; } = Guid.Empty;
        public string RawProductName { get; set; } = string.Empty;
    }
}
