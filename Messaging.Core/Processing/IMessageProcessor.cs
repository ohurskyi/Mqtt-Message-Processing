using System.Threading.Tasks;
using Messaging.Core.Commands;
using Messaging.Core.Events;

namespace Messaging.Core.Processing
{
    public interface IMessageProcessor
    {
        Task ProcessCommandRequestAsync(ICommandRequest commandRequest);
        Task ProcessIIntegrationEventAsync(IIntegrationEvent integrationEvent);
    }
}