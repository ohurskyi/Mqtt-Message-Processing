using System.Threading.Tasks;
using Messaging.Core.Messages;

namespace Messaging.Core.Processing
{
    public interface IMessageExecutor
    {
        Task ExecuteAsync(IMessage message);
    }
}
