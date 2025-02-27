namespace MonitoCalibratrice.Application.Features.ProductionBatches.DTOs
{
    public record ProductionBatchDto(
        Guid Id,
        Guid RawProductId,
        Guid VarietyId,
        string? Caliber,
        Guid FinishedProductId,
        Guid SecondaryPackagingId,
        DateTime StartTime,
        DateTime? EndTime
    );
}
