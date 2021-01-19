using System.Threading.Tasks;
using MediatR;
using Messaging.Core.Commands;
using Messaging.Core.Events;

namespace Messaging.Core.Processing
{
    public class MessageProcessor : IMessageProcessor
    {
        private readonly IMediator _mediator;

        public MessageProcessor(IMediator mediator)
        {
            _mediator = mediator;
        }

        public Task ProcessCommandRequestAsync(ICommandRequest commandRequest)
        {
            return _mediator.Send(commandRequest);
        }

        public Task ProcessIIntegrationEventAsync(IIntegrationEvent integrationEvent)
        {
            return _mediator.Publish(integrationEvent);
        }
    }
}
