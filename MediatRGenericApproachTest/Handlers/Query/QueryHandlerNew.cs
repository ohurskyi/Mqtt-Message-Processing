using MediatR;
using FluentResults;
using System.Threading.Tasks;
using System.Threading;

namespace MessageProcessorMediatR
{
    /// <summary>
    /// tes with result
    /// </summary>
    /// <typeparam name="TQuery"></typeparam>
    public abstract class QueryHandlerNew<TQuery> : IRequestHandler<TQuery, Result<IQueryResult>>
       where TQuery : IQueryWithResult
    {
        public async Task<Result<IQueryResult>> Handle(TQuery request, CancellationToken cancellationToken)
        {
            return await HandleAsync(request, cancellationToken);
        }

        protected abstract Task<Result<IQueryResult>> HandleAsync(TQuery query, CancellationToken cancellationToken);
    }
}
