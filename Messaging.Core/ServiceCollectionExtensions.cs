using Messaging.Core.Client;
using Messaging.Core.Commands;
using Messaging.Core.Commands.Implementation.InMemory;
using Messaging.Core.Processing;
using Microsoft.Extensions.DependencyInjection;

namespace Messaging.Core
{
    public static class ServiceCollectionExtensions
    {
        public static void AddInMemoryMessageProcessing(this IServiceCollection serviceCollection)
        {
            // infrastructure
            serviceCollection.AddSingleton<InMemoryQueue>();
            serviceCollection.AddSingleton<InMemoryMessageBroker>();
            serviceCollection.AddHostedService(sp => sp.GetRequiredService<InMemoryMessageBroker>());

            serviceCollection.AddTransient<ICommandBus, InMemoryCommandBus>();
            serviceCollection.AddSingleton<IMessageExecutor, ScopedMessageExecutor>();
            serviceCollection.AddTransient<IMessageProcessor, MessageProcessor>();
        }
    }
}