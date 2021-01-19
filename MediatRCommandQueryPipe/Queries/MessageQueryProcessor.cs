using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using MessageProcessorMediatR;

namespace MediatRCommandQueryPipe.Queries
{
    public interface IRequestProcessor
    {
        bool CanProcess(IRequestNew message);

        Task<IResponseResult> ProcessAsync(IRequestNew message);
    }

    public abstract class RequestProcessorBase<TRequest> : IRequestProcessor
        where TRequest: IRequestNew
    {
        public bool CanProcess(IRequestNew message)
        {
            return typeof(TRequest) == message.GetType();
        }

        Task<IResponseResult> IRequestProcessor.ProcessAsync(IRequestNew message)
        {
            return ProcessAsync((TRequest) message);
        }

        protected abstract Task<IResponseResult> ProcessAsync(TRequest message);

    }
        
    public class RequestResponseProcessor<TRequest, TCommandWithResult> : RequestProcessorBase<TRequest>
        where TRequest : IRequestNew
        where TCommandWithResult: IRequest<IResponseResult>
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        
        public RequestResponseProcessor(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }
        
        protected override async Task<IResponseResult> ProcessAsync(TRequest message)
        {
            var query = _mapper.Map<TCommandWithResult>(message);
            return await _mediator.Send(query);
        }
    }

    public interface IRequestNew
    {
        
    }

    public class TimezoneRequest : IRequestNew
    {
        
    }

    public class TcuRequest : IRequestNew
    {
        
    }

    public interface IResponse //: IRequest<IResponseResult>
    {
    }

    public interface IResponseResult
    {
    }

    public class TimezoneCommandRequest : IRequest<IResponseResult>
    {
        
    }


    public class TimezoneCommandHandler : IRequestHandler<TimezoneCommandRequest, IResponseResult>
    {
        public async Task<IResponseResult> Handle(TimezoneCommandRequest request, CancellationToken cancellationToken)
        {
            Console.WriteLine($"Query {nameof(TimezoneCommandHandler)} ...");
            var result = new TimezoneResponse();
            return await Task.FromResult(result);
        }
    }
    
    public class TcuCommandRequest : IRequest<IResponseResult>
    {
        
    }
    
    public class TcuCommandRequestCommandHandler : IRequestHandler<TcuCommandRequest, IResponseResult>
    {
        public async Task<IResponseResult> Handle(TcuCommandRequest request, CancellationToken cancellationToken)
        {
            Console.WriteLine($"Query {nameof(TcuCommandRequestCommandHandler)} ...");
            var result = new TcuResponse();
            return await Task.FromResult(result);
        }
    }

    public class TimezoneResponse : IResponseResult
    {
        
    }

    public class TcuResponse : IResponseResult
    {
        
    }
}