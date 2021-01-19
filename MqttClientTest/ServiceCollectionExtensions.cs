using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using MqttClientTest.Clients;
using MqttClientTest.Listeners;
using MqttClientTest.Messaging.Processing;
using MqttClientTest.Pipeline;
using MqttClientTest.Services;

namespace MqttClientTest
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMessagingPipeline(this IServiceCollection serviceCollection, params Assembly[] assemblies)
        {
            serviceCollection.AddSingleton<IMessageExecutor, ScopedMessageExecutor>();
            serviceCollection.AddTransient<IMessageProcessor, MessageProcessor>();
            
            serviceCollection.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
            serviceCollection.AddTransient(typeof(IPipelineBehavior<,>), typeof(PerformanceBehaviour<,>));
            serviceCollection.AddMediatR(assemblies);
            return serviceCollection;
        }

        public static IServiceCollection AddMqttMessaging(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IMqttMessagingClient, MqttMessagingClient>();
            serviceCollection.AddHostedService<MqttMessagingHostedService>();
            serviceCollection.AddHostedService<MqttMessageListener>();
            return serviceCollection;
        }
        
        public static IServiceCollection AddMqttListeners(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IMqttListener, TestMqttListener>();
            serviceCollection.AddSingleton<IMqttListener, DistributedConfigurationMqttListener>();
            return serviceCollection;
        }
    }
}