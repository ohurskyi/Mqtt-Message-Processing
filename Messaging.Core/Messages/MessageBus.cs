using System.Threading.Tasks;
using Messaging.Core.Client;
using Newtonsoft.Json;

namespace Messaging.Core.Messages
{
    public class MessageBus : IMessageBus
    {
        private readonly IQueueClient _queueClient;

        private static readonly JsonSerializerSettings JsonSerializerSettings = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.Objects
        };

        public MessageBus(IQueueClient queueClient)
        {
            _queueClient = queueClient;
        }

        public async Task SendAsync(IMessage message)
        {
            var msgJson = JsonConvert.SerializeObject(message, JsonSerializerSettings);
            await _queueClient.SendAsync(msgJson);
        }
    }
}