using MediatR;
using System.Threading.Tasks;
using System.Threading;
using System;

namespace MessageProcessorMediatR
{

    public abstract class QueryHandler<TQuery> : IRequestHandler<TQuery, IQueryResult>
        where TQuery: IQuery
    {
        public async Task<IQueryResult> Handle(TQuery request, CancellationToken cancellationToken)
        {
            return await HandleAsync(request, cancellationToken);
        }

        protected abstract Task<IQueryResult> HandleAsync(TQuery query, CancellationToken cancellationToken);
    }

    public class GetTimezoneQueryHandler : QueryHandler<GetTimezoneQuery>
    {
        protected override async Task<IQueryResult> HandleAsync(GetTimezoneQuery query, CancellationToken cancellationToken)
        {
            Console.WriteLine($"Query {nameof(GetTimezoneQueryHandler)} ...");
            var result = new TimezoneQueryResult();
            return await Task.FromResult(result);
        }
    }

    public class GetTcuConfigQueryHandler : QueryHandler<GetTcuConfigQuery>
    {
        protected override async Task<IQueryResult> HandleAsync(GetTcuConfigQuery query, CancellationToken cancellationToken)
        {
            Console.WriteLine($"Query {nameof(GetTcuConfigQueryHandler)} ...");
            var result = new TcuConfigQueryResult();
            return await Task.FromResult(result);
        }
    }
}
