using System.Threading.Tasks;
using CommandsWithResultTest.Messages;

namespace CommandsWithResultTest.Processing
{
    public interface IMessageExecutor
    {
        Task ExecuteAsync(IMessage message);
    }
}
