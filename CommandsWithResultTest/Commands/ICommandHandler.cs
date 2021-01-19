using MediatR;

namespace CommandsWithResultTest.Commands
{
    public interface ICommandHandler<in TCommandRequest, TCommandResponse> : IRequestHandler<TCommandRequest, TCommandResponse>
        where TCommandRequest: ICommandRequest<TCommandResponse>
        where TCommandResponse : ICommandResponse
    {
        
    }

    public interface ICommandRequestHandler<in TCommandRequest> : IRequestHandler<TCommandRequest, ICommandResponse>
       where TCommandRequest : ICommandRequestNonGeneric
    {

    }
}