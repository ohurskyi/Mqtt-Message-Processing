using Messaging.Core.Messages;

namespace Messaging.Core.Events
{
    public interface IEventBus : IMessageBusGeneric<IIntegrationEvent>
    {
        
    }
}