using System.Threading.Tasks;
using MqttClientTest.Messaging.Messages;
using MqttClientTest.Messaging.Processing;
using MQTTnet;
using MQTTnet.Client.Receiving;

namespace MqttClientTest.Clients
{
    public class MqttReceivedMessageHandler : IMqttApplicationMessageReceivedHandler
    {
        private readonly IMessageExecutor _messageExecutor;

        public MqttReceivedMessageHandler(IMessageExecutor messageExecutor)
        {
            _messageExecutor = messageExecutor;
        }

        public async Task HandleApplicationMessageReceivedAsync(MqttApplicationMessageReceivedEventArgs eventArgs)
        {
            var message = eventArgs.ApplicationMessage.GetMessage();
            await _messageExecutor.ExecuteAsync(message);
        }
    }
}