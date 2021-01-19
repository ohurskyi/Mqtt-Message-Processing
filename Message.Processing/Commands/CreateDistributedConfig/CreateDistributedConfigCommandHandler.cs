using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Messaging.Core.Commands;
using Microsoft.Extensions.Logging;

namespace Message.Processing.Commands.CreateDistributedConfig
{
    public class CreateDistributedConfigCommandHandler : ICommandRequestHandler<CreateDistributedConfigCommand>
    {
        private readonly ILogger<CreateDistributedConfigCommandHandler> _logger;

        public CreateDistributedConfigCommandHandler(ILogger<CreateDistributedConfigCommandHandler> logger)
        {
            _logger = logger;
        }

        public Task<Unit> Handle(CreateDistributedConfigCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Handling {request.GetType().Name}");
            //await Task.Delay(TimeSpan.FromSeconds(1), cancellationToken);
            _logger.LogInformation($"Handled {request.GetType().Name}");
            return Unit.Task;
        }
    }
}