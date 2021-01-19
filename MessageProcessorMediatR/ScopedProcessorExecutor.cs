using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace MessageProcessorMediatR
{
    public class ScopedProcessorExecutor
    {
        private readonly IServiceProvider _serviceProvider;

        public ScopedProcessorExecutor(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task ExecuteAsync(MessageTest message)
        {
            using var scope = _serviceProvider.CreateScope();
            switch (message.Type)
            {
                case MessageType.Update:
                    var updateConfigMsg = message.Msg as UpdateConfigMsg;
                    var processor1 = scope.ServiceProvider.GetRequiredService<IMessageProcessor<UpdateConfigMsg>>();
                    await processor1.ProcessAsync(updateConfigMsg);
                    break;
                case MessageType.Reset:
                    var resetConfigMsg = message.Msg as ResetConfigMsg;
                    var processor2 = scope.ServiceProvider.GetRequiredService<IMessageProcessor<ResetConfigMsg>>();
                    await processor2.ProcessAsync(resetConfigMsg);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}