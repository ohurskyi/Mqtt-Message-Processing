using MediatR;
using Message.Processing.HostedServices;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Channels;
using Messaging.Core;
using Messaging.Core.Messages;
using Microsoft.Extensions.Logging;
using Messaging.Core.Client;
using Messaging.Core.Processing;

namespace Message.Processing
{
    public class Program
    {
        static void Main()
        {
            Host.CreateDefaultBuilder()
                .ConfigureLogging(logging =>
                {
                    logging.ClearProviders();
                    logging.SetMinimumLevel(LogLevel.Information);
                    logging.AddConsole();
                })
                .ConfigureServices(ConfigureServicesInternal)
                .Build()
                .Run();
        }

        private static void ConfigureServicesInternal(IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IQueueClient, InMemoryQueueClient>();
            serviceCollection.AddTransient<IMessageBus, MessageBus>();

            // serviceCollection.AddHostedService<BackgroundEventPublisher>();
            // serviceCollection.AddHostedService<BackgroundCommandPublisher>();
            // serviceCollection.AddHostedService<BackgroundMessageConsumer>();

            
            serviceCollection.AddInMemoryMessageProcessing();
            serviceCollection.AddHostedService<BackgroundMessageConsumerNew>();
            serviceCollection.AddHostedService<BackgroundCommandPublisherNew>();
            serviceCollection.AddMediatR(typeof(Program));
        }
    }
}
