using MediatR;
using System.Threading.Tasks;
using System;

namespace MessageProcessorMediatR
{
    public class QueryProcessor<TQuery> : IQueryProcessor<TQuery>
         where TQuery : IQuery
    {
        private readonly IMediator _mediator;

        private readonly Guid _id;

        public QueryProcessor(IMediator mediator)
        {
            _mediator = mediator;
            Console.WriteLine($"{this.GetType().Name} Id = {_id = Guid.NewGuid()}");
        }

        public async Task<IQueryResult> ProcessAsync(TQuery query)
        {
            Console.WriteLine($"Begin Quering query = {query.GetType().Name}");
            var result = await _mediator.Send(query);
            Console.WriteLine($"Finishing ------------------");
            return result;
        }
    }
}
