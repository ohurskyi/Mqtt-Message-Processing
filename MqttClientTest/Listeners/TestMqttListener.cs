using System.Collections.Generic;
using MqttClientTest.Clients;

namespace MqttClientTest.Listeners
{
    public class TestMqttListener : BaseMqttListener
    {
        public TestMqttListener(IMqttMessagingClient mqttMessagingClient) : base(mqttMessagingClient)
        {
        }

        public override IEnumerable<string> Topics => new List<string>
        {
            "hello/world",
            "hello/ping"
        };
    }
}