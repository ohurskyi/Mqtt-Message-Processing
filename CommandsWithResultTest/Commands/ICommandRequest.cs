using CommandsWithResultTest.Messages;
using MediatR;

namespace CommandsWithResultTest.Commands
{
    public interface ICommandRequest<out TResponse> : IRequest<TResponse>, IMessage
        where TResponse: ICommandResponse

    {

    }

    public interface ICommandRequestNonGeneric : IRequest<ICommandResponse>, IMessage
    {

    }
}