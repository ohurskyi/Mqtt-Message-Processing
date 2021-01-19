using CommandsWithResultTest.Commands;

namespace CommandsWithResultTest.ImplementationTest.FakeCommand
{
    public class FakeCommandResponse : ICommandResponse
    {
        public string Name { get; set; } = $"{nameof(FakeCommandResponse)}";
    }
}