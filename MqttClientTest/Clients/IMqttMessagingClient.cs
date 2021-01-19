using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MQTTnet;

namespace MqttClientTest.Clients
{
    public interface IMqttMessagingClient : IDisposable
    {
        Task StartAsync();

        Task StopAsync();

        Task SubscribeAsync(string topic);

        Task SubscribeAsync(IEnumerable<string> topic);
        
        Task UnsubscribeAsync(string topic);
        
        Task UnsubscribeAsync(IEnumerable<string> topics);
        
        Task<bool> PublishAsync(MqttApplicationMessage mqttApplicationMessage);
    }
}