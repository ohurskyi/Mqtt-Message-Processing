using System.Threading.Tasks;
using MqttClientTest.Messaging.Commands;

namespace MqttClientTest.Messaging.Processing
{
    public interface IMessageProcessor
    {
        Task ProcessCommandRequestAsync(ICommandRequest commandRequest);
    }
}