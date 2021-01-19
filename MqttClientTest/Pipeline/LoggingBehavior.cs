using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;

namespace MqttClientTest.Pipeline
{
    public class LoggingBehavior<TCommandRequest, TCommandResponse> : IPipelineBehavior<TCommandRequest, TCommandResponse>
    {
        private readonly ILogger<LoggingBehavior<TCommandRequest, TCommandResponse>> _logger;

        public LoggingBehavior(ILogger<LoggingBehavior<TCommandRequest, TCommandResponse>> logger)
        {
            _logger = logger;
        }

        public async Task<TCommandResponse> Handle(TCommandRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TCommandResponse> next)
        {
            _logger.LogInformation($"Handling {typeof(TCommandRequest).Name}");
            var commandResponse = await next();
            _logger.LogInformation($"Handled {typeof(TCommandResponse).Name}");
            return commandResponse;
        }
    }
}