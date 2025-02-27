namespace MonitoCalibratrice.Application.Features.FinishedProductPallets.DTOs
{
    public record FinishedProductPalletDto(
        Guid Id,
        Guid RawProductId,
        Guid VarietyId,
        string Caliber,
        Guid FinishedProductId,
        Guid SecondaryPackagingId,
        string PalletCode,
        int Units,
        decimal Weight,
        string Annotation,
        DateTime CreatedAt
    );
}
