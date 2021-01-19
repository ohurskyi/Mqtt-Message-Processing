using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;
using Messaging.Core;
using Messaging.Core.Messages;
using Microsoft.Extensions.Logging;
using Messaging.Core.Client;
using Messaging.Core.Processing;

namespace Message.Processing.HostedServices
{
    public class BackgroundMessageConsumer : BackgroundService
    {
        private readonly IQueueClient _queueClient;
        private readonly IMessageExecutor _messageExecutor;
        private readonly ILogger _logger;

        private static readonly JsonSerializerSettings JsonSerializerSettings = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.Objects
        };

        public BackgroundMessageConsumer(IQueueClient queueClient, IMessageExecutor messageExecutor, ILogger<BackgroundMessageConsumer> logger)
        {
            _messageExecutor = messageExecutor;
            _logger = logger;
            _queueClient = queueClient;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _queueClient.ProcessMessageHandler += async (sender, msgJson) =>
            {
                var msg = JsonConvert.DeserializeObject<IMessage>(msgJson, JsonSerializerSettings);
                _logger.LogInformation($"Received {msg.GetType().Name}");
                await _messageExecutor.ExecuteAsync(msg);
                _logger.LogInformation($"{msg.GetType().Name} processed.");
                _logger.LogInformation(Environment.NewLine);
            };

            await Task.CompletedTask;
        }
    }
}
