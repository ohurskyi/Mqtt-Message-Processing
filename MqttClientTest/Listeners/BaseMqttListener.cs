using System.Collections.Generic;
using System.Threading.Tasks;
using MqttClientTest.Clients;

namespace MqttClientTest.Listeners
{
    public abstract class BaseMqttListener : IMqttListener
    {
        private readonly IMqttMessagingClient _mqttMessagingClient;

        protected BaseMqttListener(IMqttMessagingClient mqttMessagingClient)
        {
            _mqttMessagingClient = mqttMessagingClient;
        }

        public virtual async Task StartListening()
        {
            await _mqttMessagingClient.SubscribeAsync(Topics);
        }

        public virtual async Task StopListening()
        {
            await _mqttMessagingClient.UnsubscribeAsync(Topics);
        }

        public abstract IEnumerable<string> Topics { get; }
    }
}