using System.Threading;
using System.Threading.Tasks;
using FluentResults;

namespace MqttClientTest.Messaging.Commands.Test
{
    public class TestCommandHandler : ICommandRequestHandler<TestCommand>
    {
        public Task<Result> Handle(TestCommand request, CancellationToken cancellationToken)
        {
            return Task.FromResult(Result.Ok());
        }
    }
}