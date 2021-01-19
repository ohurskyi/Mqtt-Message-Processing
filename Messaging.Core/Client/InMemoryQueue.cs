using System;
using System.Threading.Channels;
using Microsoft.Extensions.Logging;

namespace Messaging.Core.Client
{
    public class InMemoryQueue : IDisposable
    {
        private readonly Channel<string> _queue = Channel.CreateUnbounded<string>();
        private readonly ILogger<InMemoryQueue> _logger;

        public InMemoryQueue(ILogger<InMemoryQueue> logger)
        {
            _logger = logger;
        }

        public ChannelWriter<string> Writer => _queue.Writer;
        
        public ChannelReader<string> Reader => _queue.Reader;

        public void Dispose()
        {
            _queue.Writer.Complete();
            _logger.LogInformation($"{nameof(InMemoryQueue)} Disposed.");
        }
    }
}