using MediatR;

namespace Messaging.Core.Commands.WithResultTest
{
    public interface ICommandHandlerWithResultNew<in TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
        where TRequest : ICommandRequestWithResponse<TResponse>
        where TResponse: ICommandResponse
    {

    }
}