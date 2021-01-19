using FluentResults;
using System.Threading.Tasks;

namespace MessageProcessorMediatR
{
    public abstract class MessageProcessorBaseTestGeneric<TMessage> : IMessageProcessorTest
        where TMessage : IMessage
    {
        public bool CanProcess(IMessage message)
        {
            return typeof(TMessage) == message.GetType();
        }

        public abstract Task<Result> ProcessAsync(TMessage message);

        Task<Result> IMessageProcessorTest.ProcessAsync(IMessage message)
        {
            return ProcessAsync((TMessage)message);
        }
    }
}
