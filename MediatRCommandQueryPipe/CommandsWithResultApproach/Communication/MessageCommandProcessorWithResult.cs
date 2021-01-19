using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using MediatRCommandQueryPipe.Messaging;
using MediatRCommandQueryPipe.Queries;

namespace MediatRCommandQueryPipe.CommandsWithResultApproach.Communication
{
    public class MessageCommandProcessorWithResult<TMessage, TCommand> : MessageCommandProcessorWithResultBase<TMessage>
        where TMessage : IMessage
        where TCommand: ICommand
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        
        public MessageCommandProcessorWithResult(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }
        
        protected override Task<ICommandResult> ProcessAsync(TMessage message)
        {
            var query = _mapper.Map<TCommand>(message);
            return _mediator.Send(query);
        }
    }
}