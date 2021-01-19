using System.Threading.Tasks;
using MediatRCommandQueryPipe.Messaging;

namespace MediatRCommandQueryPipe.CommandsWithResultApproach.Communication
{
    public interface IMessageCommandProcessorWithResult
    {
        bool CanProcess(IMessage message);

        Task<ICommandResult> ProcessAsync(IMessage message);
    }
}