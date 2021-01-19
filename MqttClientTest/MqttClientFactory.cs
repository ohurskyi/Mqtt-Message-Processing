using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MQTTnet;
using MQTTnet.Client.Options;
using MQTTnet.Extensions.ManagedClient;

namespace MqttClientTest
{
    public static class MqttClientFactory
    {
        public static IManagedMqttClient CreateMqttClient(IServiceProvider serviceProvider)
        {
            // Todo maybe wrap mqttClient to pass options before a StartAsync
            var options = serviceProvider.GetService<IOptions<ManagedMqttClientOptions>>();
            var mqttClient = new MqttFactory().CreateManagedMqttClient();
            return mqttClient;
        }
    }
}