using FluentResults;
using System.Threading.Tasks;

namespace MessageProcessorMediatR
{
    public interface IMessageProcessor<in TMessage>
        where TMessage : IMessage
    {
        Task<Result> ProcessAsync(TMessage message);
    }
}
