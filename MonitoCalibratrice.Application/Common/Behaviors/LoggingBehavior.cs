using MediatR;
using Microsoft.Extensions.Logging;

namespace MonitoCalibratrice.Application.Common.Behaviors
{
    public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
        where TResponse : Result
    {
        private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;

        public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handling {RequestName} with data: {@Request}", typeof(TRequest).Name, request);
            var response = await next();
            if (response.IsSuccess)
                _logger.LogInformation("{RequestName} handled successfully", typeof(TRequest).Name);
            else
                _logger.LogError("Error handling {RequestName}: {@Errors}", typeof(TRequest).Name, response.Errors);
            return response;
        }
    }
}
