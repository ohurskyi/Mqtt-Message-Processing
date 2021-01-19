using Message.Processing.Messages;
using Messaging.Core;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;
using Messaging.Core.Messages;
using Microsoft.Extensions.Logging;

namespace Message.Processing.HostedServices
{
    public class BackgroundEventPublisher : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger _logger;

        public BackgroundEventPublisher(IServiceProvider serviceProvider, ILogger<BackgroundEventPublisher> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while(!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(TimeSpan.FromSeconds(1), stoppingToken);

                using var scope = _serviceProvider.CreateScope();
                var messageBus = _serviceProvider.GetService<IMessageBus>();
                var msg = new PracticeInfoChanged();
                await messageBus.SendAsync(msg);
                _logger.LogInformation($"Send {msg.GetType().Name} Integration Event");
            }
        }
    }
}
