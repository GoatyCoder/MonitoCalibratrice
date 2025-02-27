namespace MonitoCalibratrice.Application.Features.Varieties.DTOs
{
    public record VarietyDto(
        Guid Id,
        string Code,
        string Name,
        string Description,
        Guid RawProductId,
        string RawProductName
    );
}
