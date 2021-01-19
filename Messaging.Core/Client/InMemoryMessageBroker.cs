using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace Messaging.Core.Client
{
    public class InMemoryMessageBroker : BackgroundService
    {
        private readonly InMemoryQueue _inMemoryQueue;
        
        public event EventHandler<string> ProcessMessageHandler;

        public InMemoryMessageBroker(InMemoryQueue inMemoryQueue)
        {
            _inMemoryQueue = inMemoryQueue;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await ProcessMessagesAsync();
        }
        
        private async Task ProcessMessagesAsync()
        {
            while (await _inMemoryQueue.Reader.WaitToReadAsync())
            {
                var serializedMsg = await _inMemoryQueue.Reader.ReadAsync();
                ProcessMessageHandler?.Invoke(this, serializedMsg);
            }
        }
    }
}