using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MqttClientTest.Messaging.Commands;
using MqttClientTest.Messaging.Messages;

namespace MqttClientTest.Messaging.Processing
{
    public class ScopedMessageExecutor : IMessageExecutor
    {
        private readonly ILogger<ScopedMessageExecutor> _logger;
        private readonly IServiceProvider _serviceProvider;

        public ScopedMessageExecutor(ILogger<ScopedMessageExecutor> logger, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        public async Task ExecuteAsync(IMessage message)
        {
            _logger.LogInformation($"Execute msg = {message.GetType().Name}");
            try
            {
                using var scope = _serviceProvider.CreateScope();
                var serviceProvider = scope.ServiceProvider;
                var messageProcessor = serviceProvider.GetRequiredService<IMessageProcessor>();
                var commandRequest = message as ICommandRequest;
                await messageProcessor.ProcessCommandRequestAsync(commandRequest);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Failed during processing msg = {message.GetType().Name}");
                throw;
            }
        }
    }
}