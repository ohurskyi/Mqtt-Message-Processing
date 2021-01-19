using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace MessageProcessorMediatR
{
    public class MessageProcessor<TMessage, TRequest> : IMessageProcessor<TMessage>, IDisposable
        where TMessage : IReceivedMsg
        where TRequest : IRequest
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        private readonly Guid _id;

        public MessageProcessor(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
            Console.WriteLine($"{this.GetType().Name} Id = {_id = Guid.NewGuid()}");
        }

        public async Task ProcessAsync(TMessage message)
        {
            if (message == null) return;
            Console.WriteLine($"Begin Processing msg = {message.GetType().Name}");
            var request = _mapper.Map<TRequest>(message);
            await _mediator.Send(request);
            Console.WriteLine($"Finishing ------------------");
        }

        public void Dispose()
        {
            Console.WriteLine($"Disposing {this.GetType().Name} = {_id}");
        }
    }

    public interface IMessageProcessor<in TMessage>
        where TMessage : IReceivedMsg
    {
        Task ProcessAsync(TMessage message);
    }
}
