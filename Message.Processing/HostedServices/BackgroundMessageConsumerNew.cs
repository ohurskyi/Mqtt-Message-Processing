using System;
using System.Threading;
using System.Threading.Tasks;
using Messaging.Core.Client;
using Messaging.Core.Messages;
using Messaging.Core.Processing;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Message.Processing.HostedServices
{
    public class BackgroundMessageConsumerNew : BackgroundService
    {
        private static readonly JsonSerializerSettings JsonSerializerSettings = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.Objects
        };

        private readonly ILogger<BackgroundMessageConsumerNew> _logger;
        private readonly InMemoryMessageBroker _inMemoryMessageBroker;
        private readonly IMessageExecutor _messageExecutor;

        public BackgroundMessageConsumerNew(
            ILogger<BackgroundMessageConsumerNew> logger,
            InMemoryMessageBroker inMemoryMessageBroker, 
            IMessageExecutor messageExecutor)
        {
            _logger = logger;
            _inMemoryMessageBroker = inMemoryMessageBroker;
            _messageExecutor = messageExecutor;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _inMemoryMessageBroker.ProcessMessageHandler += async (_, msgJson) =>
            {
                var msg = JsonConvert.DeserializeObject<IMessage>(msgJson, JsonSerializerSettings);
                await _messageExecutor.ExecuteAsync(msg);
            };

            await Task.CompletedTask;
        }
    }
}