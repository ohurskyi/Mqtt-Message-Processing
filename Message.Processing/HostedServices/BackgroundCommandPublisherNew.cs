using System;
using System.Threading;
using System.Threading.Tasks;
using Message.Processing.Commands.CreateDistributedConfig;
using Messaging.Core.Commands;
using Messaging.Core.Messages;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Message.Processing.HostedServices
{
    public class BackgroundCommandPublisherNew : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<BackgroundCommandPublisher> _logger;

        public BackgroundCommandPublisherNew(IServiceProvider serviceProvider, ILogger<BackgroundCommandPublisher> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(TimeSpan.FromSeconds(1), stoppingToken);
                
                using var scope = _serviceProvider.CreateScope();
                var msg = new CreateDistributedConfigCommand();
                var commandBus = _serviceProvider.GetService<ICommandBus>();
                await commandBus.PublishAsync(msg);
                _logger.LogInformation($"Send {msg.GetType().Name} Command");
            }
        }
    }
}