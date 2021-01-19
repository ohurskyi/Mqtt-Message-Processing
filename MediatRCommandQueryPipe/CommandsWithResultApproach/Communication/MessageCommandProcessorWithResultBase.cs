using System.Threading.Tasks;
using MediatRCommandQueryPipe.Messaging;

namespace MediatRCommandQueryPipe.CommandsWithResultApproach.Communication
{
    public abstract class MessageCommandProcessorWithResultBase<TMessage> : IMessageCommandProcessorWithResult
        where TMessage: IMessage
    {
        public bool CanProcess(IMessage message)
        {
            return typeof(TMessage) == message.GetType();
        }

        Task<ICommandResult> IMessageCommandProcessorWithResult.ProcessAsync(IMessage message)
        {
            return ProcessAsync((TMessage) message);
        }

        protected abstract Task<ICommandResult> ProcessAsync(TMessage message);

    }
}