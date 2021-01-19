using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MqttClientTest.Configurations;
using MqttClientTest.Messaging.Processing;
using MQTTnet;
using MQTTnet.Client.Options;
using MQTTnet.Client.Publishing;
using MQTTnet.Extensions.ManagedClient;
using MQTTnet.Formatter;

namespace MqttClientTest.Clients
{
    public class MqttMessagingClient : IMqttMessagingClient
    {
        private readonly IManagedMqttClient _mqttClient;
        private readonly ManagedMqttClientOptions _mqttClientOptions;
        private readonly IMessageExecutor _messageExecutor;

        public MqttMessagingClient(IOptions<MqttBrokerConnectionOptions> options, IMessageExecutor messageExecutor)
        {
            _messageExecutor = messageExecutor;

            var clientOptions = new MqttClientOptionsBuilder()
                // for addition props this protocol should be used
                .WithProtocolVersion(MqttProtocolVersion.V500)
                .WithClientId("Client1")
                .WithTcpServer(options.Value.Host, options.Value.Port)
                .Build();
                
            _mqttClientOptions = new ManagedMqttClientOptionsBuilder()
                .WithAutoReconnectDelay(TimeSpan.FromSeconds(5))
                .WithClientOptions(clientOptions)
                .Build();
            
            _mqttClient = new MqttFactory().CreateManagedMqttClient();
            ConfigureClient();
        }

        private void ConfigureClient()
        {
            _mqttClient.UseApplicationMessageReceivedHandler(new MqttReceivedMessageHandler(_messageExecutor));
        }

        public async Task StartAsync()
        {
            await _mqttClient.StartAsync(_mqttClientOptions);
        }
        
        public async Task StopAsync()
        {
            await _mqttClient.StopAsync();
        }
        
        public Task SubscribeAsync(string topic)
        {
            return _mqttClient.SubscribeAsync(topic);
        }

        public Task SubscribeAsync(IEnumerable<string> topic)
        {
            return _mqttClient.SubscribeAsync(topic.Select(t => new MqttTopicFilterBuilder().WithTopic(t).Build()));
        }
        
        public Task UnsubscribeAsync(string topic)
        {
            return _mqttClient.UnsubscribeAsync(topic);
        }

        public Task UnsubscribeAsync(IEnumerable<string> topics)
        {
            return _mqttClient.UnsubscribeAsync(topics);
        }

        public async Task<bool> PublishAsync(MqttApplicationMessage mqttApplicationMessage)
        {
            var result = await _mqttClient.PublishAsync(mqttApplicationMessage);
            return result.ReasonCode == MqttClientPublishReasonCode.Success;
        }

        public void Dispose()
        {
            Console.WriteLine("Disposing message client.");
            _mqttClient?.Dispose();
        }
    }
}