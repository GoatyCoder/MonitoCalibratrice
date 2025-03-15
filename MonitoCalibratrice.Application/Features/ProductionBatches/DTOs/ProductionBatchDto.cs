using MonitoCalibratrice.Application.Features.FinishedProductPallets.DTOs;

namespace MonitoCalibratrice.Application.Features.ProductionBatches.DTOs
{
    public record ProductionBatchDto(
        Guid Id,
        Guid ProductionLineId,
        string ProductionLineName,
        Guid RawProductId,
        string RawProductName,
        Guid VarietyId,
        string VarietyName,
        string? Caliber,
        Guid FinishedProductId,
        string FinishedProductName,
        Guid SecondaryPackagingId,
        string SecondaryPackagingName,
        int TotalUnits,
        DateTime StartedAt,
        DateTime? FinishedAt,
        ICollection<FinishedProductPalletDto> Pallets
    );
}
