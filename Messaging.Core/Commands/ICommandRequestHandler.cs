using FluentResults;
using MediatR;
using Messaging.Core.Results;
using System.Threading;
using System.Threading.Tasks;

namespace Messaging.Core.Commands
{
    public interface ICommandRequestHandler<in T> : IRequestHandler<T>
        where T: ICommandRequest
    {
        
    }
}