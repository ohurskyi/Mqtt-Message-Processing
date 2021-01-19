using FluentResults;
using MediatR;

namespace MediatRCommandQueryPipe.Commands
{
    public interface ICommandHandler<TCommand> : IRequestHandler<TCommand, Result>
        where TCommand : IRequest<Result>
    {
    }
}