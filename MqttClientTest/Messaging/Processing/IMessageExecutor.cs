using System.Threading.Tasks;
using MqttClientTest.Messaging.Messages;

namespace MqttClientTest.Messaging.Processing
{
    public interface IMessageExecutor
    {
        Task ExecuteAsync(IMessage message);
    }
}