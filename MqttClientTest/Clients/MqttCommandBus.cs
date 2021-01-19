using System.Threading.Tasks;
using MqttClientTest.Messaging.Commands;
using MqttClientTest.Messaging.Commands.Test;
using MqttClientTest.Messaging.Messages;

namespace MqttClientTest.Clients
{
    public class MqttCommandBus : ICommandBus
    {
        private readonly IMqttMessagingClient _mqttMessagingClient;

        public MqttCommandBus(IMqttMessagingClient mqttMessagingClient)
        {
            _mqttMessagingClient = mqttMessagingClient;
        }

        public async Task<bool> PublishAsync(ICommandRequest message, string topic)
        {
            var mqttMessage = message.ToMqttMessage(topic);
            return await _mqttMessagingClient.PublishAsync(mqttMessage);
        }
    }
}