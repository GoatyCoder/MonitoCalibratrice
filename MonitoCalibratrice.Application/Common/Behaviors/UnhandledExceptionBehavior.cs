using MediatR;

namespace MonitoCalibratrice.Application.Common.Behaviors
{
    public class UnhandledExceptionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
        where TResponse : Result
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            try
            {
                return await next();
            }
            catch (Exception ex)
            {
                var error = new AppError(ErrorCode.UnhandledException, ex.Message, ex.StackTrace);
                return (TResponse)Result.Failure(error);
            }
        }
    }
}
