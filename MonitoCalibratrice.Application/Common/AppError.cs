namespace MonitoCalibratrice.Application.Common
{
    public record AppError(ErrorCode Code, string Message, string? Details = null);
}
