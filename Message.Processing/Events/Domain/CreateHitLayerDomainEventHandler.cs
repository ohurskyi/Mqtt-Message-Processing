using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Message.Processing.Messages;

namespace Message.Processing.Events.Domain
{
    public class CreateHitLayerDomainEventHandler : INotificationHandler<TargetLayerCreated>
    {
        private readonly IMediator _mediator;

        public CreateHitLayerDomainEventHandler(IMediator mediator)
        {
            this._mediator = mediator;
        }

        public Task Handle(TargetLayerCreated notification, CancellationToken cancellationToken)
        {
            Console.WriteLine($"Handling {nameof(TargetLayerCreated)}");
            Console.WriteLine($"Created Hit Layer.");
            Console.WriteLine($"Handled {nameof(TargetLayerCreated)}");
            return Task.CompletedTask;
        }
    }
}
