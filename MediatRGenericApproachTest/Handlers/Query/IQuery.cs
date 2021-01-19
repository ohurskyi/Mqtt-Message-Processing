using FluentResults;
using MediatR;

namespace MessageProcessorMediatR
{  
    /// <summary>
    /// test with result
    /// </summary>
    public interface IQueryWithResult : IRequest<Result<IQueryResult>>
    {

    }

    public interface IQuery : IRequest<IQueryResult>
    {

    }

    public class GetTimezoneQuery : IQuery
    {

    }

    public class GetTcuConfigQuery : IQuery
    {

    }
}