namespace MonitoCalibratrice.Application.Features.FinishedProducts.DTOs
{
    public record FinishedProductDto
    (
        Guid Id,
        string Code,
        string Name,
        string? Description,
        string? Ean
    );
}
