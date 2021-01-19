using AutoMapper;
using FluentResults;
using MediatR;
using System;
using System.Threading.Tasks;

namespace MessageProcessorMediatR
{
    public class MessageCommandProcessorTest<TMessage, TRequest> : MessageProcessorBaseTestGeneric<TMessage>
        where TMessage : IMessage
        where TRequest : IRequest<Result>
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        private readonly Guid _id;

        public MessageCommandProcessorTest(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
            Console.WriteLine($"{this.GetType().Name} Id = {_id = Guid.NewGuid()}");
        }

        public override async Task<Result> ProcessAsync(TMessage message)
        {
            Console.WriteLine($"Begin Processing msg = {message.GetType().Name}");
            var request = _mapper.Map<TRequest>(message);
            var result = await _mediator.Send(request);
            Console.WriteLine($"Finishing ------------------");
            return result;
        }
    }
}
