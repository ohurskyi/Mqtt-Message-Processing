using MediatR;

namespace Messaging.Core.Commands.WithResultTest
{
    public interface ICommandRequestWithResponse<out TResponse> : IRequest<TResponse>
        where TResponse: ICommandResponse
    {
    }
}