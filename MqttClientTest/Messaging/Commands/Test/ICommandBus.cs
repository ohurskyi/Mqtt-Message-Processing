using MqttClientTest.Messaging.Messages;

namespace MqttClientTest.Messaging.Commands.Test
{
    public interface ICommandBus : IMessageBus<ICommandRequest>
    {
        
    }
}