using Messaging.Core.Commands;

namespace Message.Processing.Commands.CreateDistributedConfig
{
    public class CreateDistributedConfigCommand : ICommandRequest
    {
        public string Name { get; set; } = $"{nameof(CreateDistributedConfigCommand)}";
    }
}