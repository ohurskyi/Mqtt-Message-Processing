using System.Threading.Tasks;
using FluentResults;

namespace MediatRCommandQueryPipe.Messaging
{
    public interface IMessageProcessor
    {
        bool CanProcess(IMessage message);
        Task<Result> ProcessAsync(IMessage message);
    }
}