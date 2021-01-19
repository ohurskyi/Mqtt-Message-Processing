using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Message.Processing.Messages;
using Messaging.Core.Events;

namespace Message.Processing.Events.Integration
{
    public class UpdateShotsListIntegrationEventHandler : IIntegrationEventHandler<PracticeInfoChanged>
    {
        public Task Handle(PracticeInfoChanged notification, CancellationToken cancellationToken)
        {
            Console.WriteLine($"Handling {nameof(PracticeInfoChanged)}");
            Console.WriteLine($"Updated Shots List.");
            Console.WriteLine($"Handled {nameof(PracticeInfoChanged)}");
            return Task.CompletedTask;
        }
    }
}