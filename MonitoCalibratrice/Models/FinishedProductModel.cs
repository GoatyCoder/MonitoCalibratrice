﻿namespace MonitoCalibratrice.Models
{
    public class FinishedProductModel
    {
        public Guid Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? Ean { get; set; }
    }
}
