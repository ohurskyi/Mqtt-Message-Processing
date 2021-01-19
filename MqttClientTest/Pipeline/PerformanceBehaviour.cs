using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;

namespace MqttClientTest.Pipeline
{
    public class PerformanceBehaviour<TCommandRequest, TCommandResponse> : IPipelineBehavior<TCommandRequest, TCommandResponse>
    {
        private readonly ILogger<PerformanceBehaviour<TCommandRequest, TCommandResponse>> _logger;

        public PerformanceBehaviour(ILogger<PerformanceBehaviour<TCommandRequest, TCommandResponse>> logger)
        {
            _logger = logger;
        }

        public async Task<TCommandResponse> Handle(TCommandRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TCommandResponse> next)
        {
            var stopWatch = Stopwatch.StartNew();
            var response = await next();
            stopWatch.Stop();
            _logger.LogInformation($"Request execution time take {stopWatch.Elapsed.TotalMilliseconds} milliseconds");
            return response;
        }
    }
}