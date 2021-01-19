using CommandsWithResultTest.Commands;

namespace CommandsWithResultTest.ImplementationTest.FakeCommand
{
    public class FakeCommandRequest : ICommandRequest<FakeCommandResponse>
    {
        public string Data { get; set; } = $"{nameof(FakeCommandRequest)}";
    }

    public class FakeCommandRequestNonGeneric : ICommandRequestNonGeneric
    {
        public string Data { get; set; } = $"{nameof(FakeCommandRequestNonGeneric)}";
    }
}