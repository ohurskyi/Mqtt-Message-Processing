using System;
using System.Threading;
using System.Threading.Tasks;
using MessageProcessorMediatR;

namespace MediatRCommandQueryPipe.Queries
{
    public class GetTimezoneQueryHandler : IQueryHandler<GetTimezoneQuery>
    {
        public async Task<IQueryResult> Handle(GetTimezoneQuery request, CancellationToken cancellationToken)
        {
            Console.WriteLine($"Query {nameof(GetTimezoneQueryHandler)} ...");
            var result = new TimezoneQueryResult();
            return await Task.FromResult(result);
        }
    }
}