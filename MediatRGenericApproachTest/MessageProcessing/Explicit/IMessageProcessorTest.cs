using FluentResults;
using System.Threading.Tasks;

namespace MessageProcessorMediatR
{
    /// <summary>
    /// test region
    /// </summary>
    /// 
    public interface IMessageProcessorTest
    {
        Task<Result> ProcessAsync(IMessage message);
        bool CanProcess(IMessage message);
    }
}
