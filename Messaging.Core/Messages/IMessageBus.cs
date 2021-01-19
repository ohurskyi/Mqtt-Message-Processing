using System.Threading.Tasks;

namespace Messaging.Core.Messages
{
    public interface IMessageBus
    {
        Task SendAsync(IMessage message);
    }
    
    public interface IMessageBusGeneric<in T>
        where T: IMessage
    {
        Task PublishAsync(T message);
    }
}
