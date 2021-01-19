using System.Collections.Generic;
using MqttClientTest.Clients;

namespace MqttClientTest.Listeners
{
    public class DistributedConfigurationMqttListener : BaseMqttListener
    {
        public DistributedConfigurationMqttListener(IMqttMessagingClient mqttMessagingClient) : base(mqttMessagingClient)
        {
        }

        public override IEnumerable<string> Topics => new List<string>
        {
            "config/world",
            "config/ping"
        };
    }
}