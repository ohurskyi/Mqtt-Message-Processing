using System;
using System.Threading;
using System.Threading.Tasks;

namespace Messaging.Core.Client
{
    public interface IQueueClient
    {
        ValueTask SendAsync(string serializedMsg, CancellationToken cancellationToken = default);
        
        event EventHandler<string> ProcessMessageHandler;
    }
}
