using System.Threading.Tasks;
using CommandsWithResultTest.Commands;
using MediatR;

namespace CommandsWithResultTest.Processing
{
    public class MessageProcessor : IMessageProcessor
    {
        private readonly IMediator _mediator;

        public MessageProcessor(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<TCommandResponse> ProcessCommandRequestAsync<TCommandRequest, TCommandResponse>(TCommandRequest commandRequest) 
            where TCommandRequest : ICommandRequest<TCommandResponse> 
            where TCommandResponse : ICommandResponse
        {
            var commandResponse = await _mediator.Send(commandRequest);
            return commandResponse;
        }

        public async Task<ICommandResponse> ProcessCommandRequestAsync(ICommandRequestNonGeneric commandRequest)
        {
            var commandResponse = await _mediator.Send(commandRequest);
            return commandResponse;
        }
    }
}
