using MediatR;
using System;
using System.Threading.Tasks;

namespace MessageProcessorMediatR
{
    public interface IQueryProcessor<TQuery>
        where TQuery : IQuery
    {
        Task<IQueryResult> ProcessAsync(TQuery query);
    }

    public interface IQueryProcessorTest
    {
        bool CanProcess(IQuery query);
        Task<IQueryResult> ProcessAsync(IQuery query);
    }

    public abstract class QueryProcessorTestBase<TQuery> : IQueryProcessorTest
         where TQuery : IQuery
    {
        public bool CanProcess(IQuery query)
        {
            return typeof(TQuery) == query.GetType();
        }

        protected abstract Task<IQueryResult> ProcessAsync(TQuery query);

        Task<IQueryResult> IQueryProcessorTest.ProcessAsync(IQuery query)
        {
            return ProcessAsync((TQuery)query);
        }
    }

    public class QueryProcessorTest<TQuery> : QueryProcessorTestBase<TQuery>
        where TQuery : IQuery
    {
        private readonly IMediator _mediator;

        private readonly Guid _id;

        public QueryProcessorTest(IMediator mediator)
        {
            _mediator = mediator;
            Console.WriteLine($"{this.GetType().Name} Id = {_id = Guid.NewGuid()}");
        }

        protected override async Task<IQueryResult> ProcessAsync(TQuery query)
        {
            Console.WriteLine($"Begin Quering query = {query.GetType().Name}");
            var result = await _mediator.Send(query);
            Console.WriteLine($"Finishing ------------------");
            return result;
        }
    }
}
