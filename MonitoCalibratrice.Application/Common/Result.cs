namespace MonitoCalibratrice.Application.Common
{
    public record Result(bool IsSuccess, IReadOnlyCollection<AppError>? Errors)
    {
        public static Result Success() => new Result(true, null);
        public static Result Failure(IEnumerable<AppError> errors) => new Result(false, errors.ToList());
        public static Result Failure(AppError error) => new Result(false, new List<AppError> { error });
    }

    public record Result<T>(bool IsSuccess, T? Value, IReadOnlyCollection<AppError>? Errors) : Result(IsSuccess, Errors)
    {
        public static Result<T> Success(T value) => new Result<T>(true, value, null);
        public new static Result<T> Failure(IEnumerable<AppError> errors) => new Result<T>(false, default, errors.ToList());
        public new static Result<T> Failure(AppError error) => new Result<T>(false, default, new List<AppError> { error });
    }
}
