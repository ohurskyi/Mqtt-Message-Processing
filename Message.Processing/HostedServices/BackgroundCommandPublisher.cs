using System;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;
using Message.Processing.Commands.CreateDistributedConfig;
using Message.Processing.Messages;
using Messaging.Core.Messages;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Message.Processing.HostedServices
{
    public class BackgroundCommandPublisher : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<BackgroundCommandPublisher> _logger;

        public BackgroundCommandPublisher(IServiceProvider serviceProvider, ILogger<BackgroundCommandPublisher> logger)
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
                var messageBus = _serviceProvider.GetService<IMessageBus>();
                var msg = new CreateDistributedConfigCommand();
                await messageBus.SendAsync(msg);
                _logger.LogInformation($"Send {msg.GetType().Name} Command");
            }
        }
    }
}