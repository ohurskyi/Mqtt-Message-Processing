using MediatR;
using MessageProcessorMediatR;

namespace MediatRCommandQueryPipe.Queries
{
    public interface IQuery : IRequest<IQueryResult>
    {
        
    }
}