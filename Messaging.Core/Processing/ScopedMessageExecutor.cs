using System;
using System.Threading.Tasks;
using Messaging.Core.Commands;
using Messaging.Core.Events;
using Messaging.Core.Messages;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Messaging.Core.Processing
{
    public class ScopedMessageExecutor : IMessageExecutor
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<ScopedMessageExecutor> _logger;

        public ScopedMessageExecutor(IServiceProvider serviceProvider, ILogger<ScopedMessageExecutor> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        public async Task ExecuteAsync(IMessage message)
        {
            try
            {
                using var scope = _serviceProvider.CreateScope();
                var messageProcessor = scope.ServiceProvider.GetService<IMessageProcessor>();
                await ProcessMessageAsync(message);

                async Task ProcessMessageAsync(IMessage msg)
                {
                    var task = msg switch
                    {
                        IIntegrationEvent integrationEvent => messageProcessor.ProcessIIntegrationEventAsync(integrationEvent),
                        ICommandRequest commandRequest => messageProcessor.ProcessCommandRequestAsync(commandRequest),
                        _ => throw new ArgumentException($"Invalid message type '{msg.GetType()}'."),
                    };

                    await task;
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Error while processing {message.GetType()}");
            }
        }
    }
}