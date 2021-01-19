using System.Threading;
using System.Threading.Tasks;
using CommandsWithResultTest.Commands;
using MediatR;

namespace CommandsWithResultTest.ImplementationTest.FakeCommand
{
    public class FakeCommandHandler : ICommandHandler<FakeCommandRequest, FakeCommandResponse>
    {
        public Task<FakeCommandResponse> Handle(FakeCommandRequest request, CancellationToken cancellationToken)
        {
            return Task.FromResult(new FakeCommandResponse());
        }
    }

    public class FakeCommandHandlerNonGeneric : ICommandRequestHandler<FakeCommandRequestNonGeneric>
    {
        public async Task<ICommandResponse> Handle(FakeCommandRequestNonGeneric request, CancellationToken cancellationToken)
        {
            return await Task.FromResult(new FakeCommandResponse());
        }
    }
}