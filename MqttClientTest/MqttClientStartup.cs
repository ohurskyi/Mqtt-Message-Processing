using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using MqttClientTest.Messaging.Messages;
using MqttClientTest.Messaging.Processing;
using MQTTnet;
using MQTTnet.Client.Receiving;
using MQTTnet.Diagnostics;
using MQTTnet.Extensions.ManagedClient;

namespace MqttClientTest
{
    public class MqttClientStartup: IHostedService
    {
        private readonly IManagedMqttClient _mqttClient;
        private readonly IServiceProvider _serviceProvider;
        private readonly IMessageExecutor _messageExecutor;

        public MqttClientStartup(IManagedMqttClient mqttClient, IServiceProvider serviceProvider, IMessageExecutor messageExecutor)
        {
            _mqttClient = mqttClient;
            _serviceProvider = serviceProvider;
            _messageExecutor = messageExecutor;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            var options = _serviceProvider.GetService<IOptions<ManagedMqttClientOptions>>();
            
            await _mqttClient.StartAsync(options.Value);
            
            Trace();

            var helloWorldTopic = new MqttTopicFilterBuilder().WithTopic("hello/world").WithAtLeastOnceQoS().Build();
            var pingTopic = new MqttTopicFilterBuilder().WithTopic("hello/ping").WithAtLeastOnceQoS().Build();
            await _mqttClient.SubscribeAsync(helloWorldTopic, pingTopic);

            static void ReceivedHandler(MqttApplicationMessageReceivedEventArgs msg)
            {
                Console.WriteLine($"Calling {nameof(ReceivedHandler)} msg = {msg.ClientId} ... time = {DateTime.Now.TimeOfDay:g}");
                var payload = Encoding.UTF8.GetString(msg.ApplicationMessage.Payload);
                Console.WriteLine($"Message {payload} received on topic = {msg.ApplicationMessage.Topic}");
                Console.WriteLine($"Receive done!!!");
            }

            static void ProcessedHandler(ApplicationMessageProcessedEventArgs msg)
            {
                Console.WriteLine($"Calling {nameof(ProcessedHandler)} msg = {msg.ApplicationMessage.Id} ... time = {DateTime.Now.TimeOfDay:g}");
                var payload = Encoding.UTF8.GetString(msg.ApplicationMessage.ApplicationMessage.Payload);
                Console.WriteLine($"Message processed on topic = {msg.ApplicationMessage.ApplicationMessage.Topic}");
                Console.WriteLine($"Processed done!!!");
            }

            // todo what the difference ApplicationMessageReceivedHandler vs ApplicationMessageProcessedHandler
            // looks like the reason is because for publish and receive we are using the same client
            async Task ReceivedHandlerAsync(MqttApplicationMessageReceivedEventArgs msg)
            {
                Console.WriteLine($"Calling {nameof(ReceivedHandlerAsync)} msg = {msg.ClientId} ...");
                await Task.Delay(TimeSpan.FromSeconds(1), cancellationToken);
                var payload = Encoding.UTF8.GetString(msg.ApplicationMessage.Payload);
                Console.WriteLine($"Message received on topic = {msg.ApplicationMessage.Topic}");
            }

            // called when client has processed the msg
            async Task ProcessedHandlerAsync(ApplicationMessageProcessedEventArgs msg)
            {
                Console.WriteLine($"Calling {nameof(ProcessedHandlerAsync)} msg = {msg.ApplicationMessage.Id} ...");
                await Task.Delay(TimeSpan.FromSeconds(1), cancellationToken);
                var payload = Encoding.UTF8.GetString(msg.ApplicationMessage.ApplicationMessage.Payload);
                Console.WriteLine($"Message processed on topic = {msg.ApplicationMessage.ApplicationMessage.Topic}");
            }

            // _mqttClient.ApplicationMessageProcessedHandler =
            //     new ApplicationMessageProcessedHandlerDelegate(MessageHandler);

            _mqttClient.ApplicationMessageReceivedHandler = new MqttApplicationMessageReceivedHandlerDelegate(MessageHandler);
            // called before ApplicationMessageReceivedHandler
            // I think this means message was processed by broker and passed to client then ApplicationMessageReceivedHandler are called
            //_mqttClient.ApplicationMessageProcessedHandler = new ApplicationMessageProcessedHandlerDelegate(ProcessedHandler);
            
            async Task MessageHandler(MqttApplicationMessageReceivedEventArgs msg)
            {
                Console.WriteLine($"Client = {msg.ClientId} received msg");
                Console.WriteLine($"Received on topic = {msg.ApplicationMessage.Topic}");
                var message = msg.ApplicationMessage.GetMessage();
                await _messageExecutor.ExecuteAsync(message);
            }
            // can be only one
            //_mqttClient.UseApplicationMessageReceivedHandler(Handler2);
        }

        private static void Trace()
        {
            // MqttNetGlobalLogger.LogMessagePublished += (s, e) =>
            // {
            //     Console.WriteLine(e.LogMessage);
            // };
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await _mqttClient.StopAsync();
            // container should dispose!
            _mqttClient.Dispose();
        }
    }
}