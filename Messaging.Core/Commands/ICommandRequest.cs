using MediatR;
using Messaging.Core.Messages;

namespace Messaging.Core.Commands
{
    public interface ICommandRequest : IRequest, IMessage
    {
    }
}
