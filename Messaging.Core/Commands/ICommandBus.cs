using System.Threading.Tasks;
using Messaging.Core.Messages;

namespace Messaging.Core.Commands
{
    public interface ICommandBus : IMessageBusGeneric<ICommandRequest>
    {
        Task<TCommandResponse> PublishAsync<TCommandResponse, TCommandRequest>(TCommandRequest commandRequest)
            where TCommandRequest : ICommandRequest
            where TCommandResponse : ICommandResponse;
    }
}