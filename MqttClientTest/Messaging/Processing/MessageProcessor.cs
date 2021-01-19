using System.Threading.Tasks;
using MediatR;
using MqttClientTest.Messaging.Commands;

namespace MqttClientTest.Messaging.Processing
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
    }
}
