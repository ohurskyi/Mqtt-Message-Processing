using MediatR;

namespace Messaging.Core.Commands.WithResultTest
{
    public interface ICommandHandlerWithResult<in TRequest> : IRequestHandler<TRequest, ICommandResponse>
        where TRequest : ICommandRequestWithResponseTest
    {

    }
}