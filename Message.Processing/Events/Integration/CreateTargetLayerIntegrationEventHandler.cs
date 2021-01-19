using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Message.Processing.Messages;
using Messaging.Core.Events;

namespace Message.Processing.Events.Integration
{
    public class CreateTargetLayerIntegrationEventHandler : IIntegrationEventHandler<PracticeInfoChanged>
    {
        private readonly IMediator _mediator;

        public CreateTargetLayerIntegrationEventHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Handle(PracticeInfoChanged notification, CancellationToken cancellationToken)
        {
            Console.WriteLine($"Handling {nameof(PracticeInfoChanged)}");
            Console.WriteLine($"Creating Target Layer.");
            await _mediator.Publish(new TargetLayerCreated(), cancellationToken);
            Console.WriteLine($"Handled {nameof(PracticeInfoChanged)}");
        }
    }
}
