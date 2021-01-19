using System.Threading.Tasks;
using FluentResults;

namespace MediatRCommandQueryPipe.Messaging
{
    public abstract class MessageProcessorBase<TMessage> : IMessageProcessor
        where TMessage : IMessage
    {
        public bool CanProcess(IMessage message)
        {
            return typeof(TMessage) == message.GetType();
        }

        Task<Result> IMessageProcessor.ProcessAsync(IMessage message)
        {
            return ProcessAsync((TMessage) message);
        }

        protected abstract Task<Result> ProcessAsync(IMessage message);

    }
}