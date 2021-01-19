using MediatR;
using Messaging.Core.Messages;

namespace Messaging.Core.Events
{
    public interface IIntegrationEvent : INotification, IMessage
    {
        
    }
}
