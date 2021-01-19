using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MediatRCommandQueryPipe.Queries;

namespace MediatRCommandQueryPipe.CommandsWithResultApproach
{
    public interface ICommand : IRequest<ICommandResult>
    {
        
    }
}