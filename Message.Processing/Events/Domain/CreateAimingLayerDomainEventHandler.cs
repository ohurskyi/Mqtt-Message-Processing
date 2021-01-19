using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Message.Processing.Messages;

namespace Message.Processing.Events.Domain
{
    public class CreateAimingLayerDomainEventHandler : INotificationHandler<TargetLayerCreated>
    {
        public Task Handle(TargetLayerCreated notification, CancellationToken cancellationToken)
        {
            Console.WriteLine($"Handling {nameof(TargetLayerCreated)}");
            Console.WriteLine($"Created Aiming Layer.");
            Console.WriteLine($"Handled {nameof(TargetLayerCreated)}");
            return Task.CompletedTask;
        }
    }
}