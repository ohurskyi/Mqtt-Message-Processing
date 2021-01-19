using MediatR;

namespace MediatRCommandQueryPipe.CommandsWithResultApproach
{
    public interface ICommandHandler<TCommand> : IRequestHandler<TCommand, ICommandResult>
        where TCommand : ICommand
    {
    }
}