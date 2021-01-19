using MediatR;

namespace Messaging.Core.Events
{
    public interface IIntegrationEventHandler<in T> : INotificationHandler<T>
        where T: IIntegrationEvent
    {
        
    }
}