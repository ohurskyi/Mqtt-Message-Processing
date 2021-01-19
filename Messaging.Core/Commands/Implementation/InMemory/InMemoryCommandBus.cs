using System.Threading;
using System.Threading.Tasks;
using Messaging.Core.Client;
using Newtonsoft.Json;

namespace Messaging.Core.Commands.Implementation.InMemory
{
    public class InMemoryCommandBus : ICommandBus
    {
        private static readonly JsonSerializerSettings JsonSerializerSettings = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.Objects
        };
        
        private readonly InMemoryQueue _inMemoryQueue;

        public InMemoryCommandBus(InMemoryQueue inMemoryQueue)
        {
            _inMemoryQueue = inMemoryQueue;
        }

        public async Task PublishAsync(ICommandRequest message)
        {
            var msgJson = JsonConvert.SerializeObject(message, JsonSerializerSettings);
            await _inMemoryQueue.Writer.WriteAsync(msgJson);
        }

        public Task<TCommandResponse> PublishAsync<TCommandResponse, TCommandRequest>(TCommandRequest commandRequest) 
            where TCommandResponse : ICommandResponse where TCommandRequest : ICommandRequest
        {
            throw new System.NotImplementedException();
        }
    }
}