using System.Threading.Tasks;
using AutoMapper;
using FluentResults;
using MediatR;
using MediatRCommandQueryPipe.Messaging;

namespace MediatRCommandQueryPipe.Commands
{
    public class MessageCommandProcessor<TMessage, TCommand> : MessageProcessorBase<TMessage>
        where TMessage : IMessage
        where TCommand: ICommand
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        
        public MessageCommandProcessor(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }
        protected override Task<Result> ProcessAsync(IMessage message)
        {
            var command = _mapper.Map<TCommand>(message);
            return _mediator.Send(command);
        }
    }
}