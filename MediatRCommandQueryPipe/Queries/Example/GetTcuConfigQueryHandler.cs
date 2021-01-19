using System;
using System.Threading;
using System.Threading.Tasks;
using MessageProcessorMediatR;

namespace MediatRCommandQueryPipe.Queries
{
    public class GetTcuConfigQueryHandler : IQueryHandler<GetTcuConfigQuery>
    {
        public async Task<IQueryResult> Handle(GetTcuConfigQuery request, CancellationToken cancellationToken)
        {
            Console.WriteLine($"Query {nameof(GetTcuConfigQueryHandler)} ...");
            var result = new TcuConfigQueryResult();
            return await Task.FromResult(result);
        }
    }
}