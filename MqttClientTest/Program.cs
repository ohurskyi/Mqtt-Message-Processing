using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MqttClientTest.Clients;
using MqttClientTest.Configurations;
using MqttClientTest.Messaging.Commands.Test;

namespace MqttClientTest
{
    class Program
    {
        private static async Task Main(string[] args)
        {
            var host = Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, collection) =>
                {
                    //collection.AddSingleton(MqttClientFactory.CreateMqttClient);
                    // orders here matters, as we cannot publish messages before _mqttClient.StartAsync is called
                    //collection.AddHostedService<MqttClientStartup>();
                    // collection.AddHostedService<BackgroundMqttMessagePublisher>();

                    collection.Configure<MqttBrokerConnectionOptions>(
                        hostContext.Configuration.GetSection(MqttBrokerConnectionOptions.MqttBrokerConnection));

                    collection
                        // or AddSingleton?
                        .AddTransient<ICommandBus, MqttCommandBus>()
                        .AddMqttMessaging()
                        .AddMqttListeners()
                        .AddMessagingPipeline(typeof(Program).Assembly);

                    collection.AddHostedService<BackgroundMqttMessagePublisher>();
                }).Build();
            await host.RunAsync();
        }
    }
}