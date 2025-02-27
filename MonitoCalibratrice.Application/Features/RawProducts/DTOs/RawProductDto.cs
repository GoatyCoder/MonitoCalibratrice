namespace MonitoCalibratrice.Application.Features.RawProducts.DTOs
{
    public record RawProductDto
    (
        Guid Id,
        string Code,
        string Name,
        string Description
    );
}
