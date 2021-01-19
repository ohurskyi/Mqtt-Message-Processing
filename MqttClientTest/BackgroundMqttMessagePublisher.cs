using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MqttClientTest.Messaging.Commands.Test;
using MqttClientTest.Messaging.Messages;
using MQTTnet;
using Newtonsoft.Json;

namespace MqttClientTest
{
    public class BackgroundMqttMessagePublisher : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private int _msgSendCount = 0;

        public BackgroundMqttMessagePublisher(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using var scope = _serviceProvider.CreateScope();
                var serviceProvider = scope.ServiceProvider;
                
                var message = new TestCommand {TestData = $"hello {++_msgSendCount}"};
                const string topic = "hello/world";
                var messageBus = serviceProvider.GetRequiredService<ICommandBus>();
                await messageBus.PublishAsync(message, topic);
                
                await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
            }
        }
    }
}