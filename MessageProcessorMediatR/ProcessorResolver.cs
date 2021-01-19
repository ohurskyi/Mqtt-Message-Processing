using System;
using Microsoft.Extensions.DependencyInjection;

namespace MessageProcessorMediatR
{
    public class ProcessorResolver
    {
        private readonly IServiceProvider _serviceProvider;

        public ProcessorResolver(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        
        public static T Resolve<T, U>(MessageType messageType, IServiceProvider serviceProvider) 
            where T: IMessageProcessor<U>
            where U: IReceivedMsg
        {
            switch (messageType)
            {
                case MessageType.Update:
                    var processor1 = serviceProvider.GetRequiredService<IMessageProcessor<UpdateConfigMsg>>();
                    return (T) processor1;
                case MessageType.Reset:
                    var processor2 = serviceProvider.GetRequiredService<IMessageProcessor<ResetConfigMsg>>();
                    return (T) processor2;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}