using MediatR;

namespace Messaging.Core.Commands
{
    public interface ICommandRequestWithResponseTest : IRequest<ICommandResponse>
    {
    }
}