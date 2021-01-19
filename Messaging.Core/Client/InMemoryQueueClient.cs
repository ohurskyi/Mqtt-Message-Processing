using System;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace Messaging.Core.Client
{
    public class InMemoryQueueClient : IQueueClient
    {
        private readonly Channel<string> _queue = Channel.CreateUnbounded<string>();

        public event EventHandler<string> ProcessMessageHandler;

        public InMemoryQueueClient()
        {
            Task.Factory.StartNew(ProcessMessagesAsync, default, TaskCreationOptions.LongRunning, TaskScheduler.Default);
        }

        public ValueTask SendAsync(string serializedMsg, CancellationToken cancellationToken)
        {
            return _queue.Writer.WriteAsync(serializedMsg, cancellationToken);
        }

        private async Task ProcessMessagesAsync()
        {
            while (await _queue.Reader.WaitToReadAsync())
            {
                var serializedMsg = await _queue.Reader.ReadAsync();
                ProcessMessageHandler?.Invoke(this, serializedMsg);
            }
        }
    }
}