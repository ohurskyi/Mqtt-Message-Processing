using MediatR;
using System;
using System.Threading.Tasks;
using AutoMapper;
using MediatRCommandQueryPipe.Messaging;
using MediatRCommandQueryPipe.Queries;

namespace MessageProcessorMediatR
{
    
    public interface IQueryProcessor
    {
        bool CanProcess(IQuery query);
        Task<IQueryResult> ProcessAsync(IQuery query);
    }

    public abstract class QueryProcessorBase<TQuery> : IQueryProcessor
         where TQuery : IQuery
    {
        public bool CanProcess(IQuery query)
        {
            return typeof(TQuery) == query.GetType();
        }

        protected abstract Task<IQueryResult> ProcessAsync(TQuery query);

        Task<IQueryResult> IQueryProcessor.ProcessAsync(IQuery query)
        {
            return ProcessAsync((TQuery)query);
        }
    }

    public class QueryProcessor<TMessage, TQuery> : QueryProcessorBase<TQuery>
        where TMessage: IMessage
        where TQuery : IQuery
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        private readonly Guid _id;

        public QueryProcessor(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        protected override async Task<IQueryResult> ProcessAsync(TQuery query)
        {
            var result = await _mediator.Send(query);
            return result;
        }
    }
}
