namespace MonitoCalibratrice.Application.Features.SecondaryPackagings.DTOs
{
    public record SecondaryPackagingDto(
        Guid Id,
        string Code,
        string Name,
        string Description
    );
}
