using MediatR;
using MessageProcessorMediatR;

namespace MediatRCommandQueryPipe.Queries
{

    public interface IQueryHandler<TQuery> : IRequestHandler<TQuery, IQueryResult>
        where TQuery: IQuery
    {
    }
}
