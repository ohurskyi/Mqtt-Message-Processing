using FluentResults;
using MediatR;
using MqttClientTest.Messaging.Messages;

namespace MqttClientTest.Messaging.Commands
{
    public interface ICommandRequest : IRequest<Result>, IMessage
    {
        
    }
}