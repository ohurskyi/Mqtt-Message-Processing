using FluentResults;
using MediatR;

namespace MqttClientTest.Messaging.Commands
{
    public interface ICommandRequestHandler<in TCommandRequest> : IRequestHandler<TCommandRequest, Result>
        where TCommandRequest : ICommandRequest
    {
        
    }
}