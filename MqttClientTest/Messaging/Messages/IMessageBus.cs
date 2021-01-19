using System.Threading.Tasks;

namespace MqttClientTest.Messaging.Messages
{
    public interface IMessageBus<in T> where T: IMessage
    {
        Task<bool> PublishAsync(T message, string topic);
    }
}