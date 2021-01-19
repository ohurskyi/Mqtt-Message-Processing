using System;
using System.Linq;
using System.Threading.Tasks;
using CommandsWithResultTest.Commands;
using CommandsWithResultTest.ImplementationTest.FakeCommand;
using CommandsWithResultTest.Messages;
using CommandsWithResultTest.Processing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace CommandsWithResultTest.Processing
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
                var messageProcessor = _serviceProvider.GetService<IMessageProcessor>();
                var commandRequest = message as ICommandRequestNonGeneric;
                var commandResponse = await messageProcessor.ProcessCommandRequestAsync(commandRequest);
                var test = commandResponse as FakeCommandResponse;
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Error while processing {message.GetType()}");
            }
        }
    }
}