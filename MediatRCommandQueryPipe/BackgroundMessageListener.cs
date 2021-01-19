using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentResults;
using MediatRCommandQueryPipe.CommandsWithResultApproach;
using MediatRCommandQueryPipe.CommandsWithResultApproach.Communication;
using MediatRCommandQueryPipe.Messaging;
using MediatRCommandQueryPipe.Messaging.MessageExamples;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;

namespace MediatRCommandQueryPipe
{

    public class BackgroundMessageListener : BackgroundService
    {
        private static readonly JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.Objects
        };
        
        private static readonly List<IMessage> messages = new List<IMessage>
        {
            new GetTimezoneMessage { Timezone = "Lviv" },
            new GetTcuConfigurationMessage { ButtonId = "Button id", TcuId = "A"}
        };

        private readonly IServiceProvider _serviceProvider;

        public BackgroundMessageListener(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(TimeSpan.FromSeconds(1), stoppingToken);

                var jsonResponse = await GenerateMessages();
                var deserializedCommandResult = JsonConvert.DeserializeObject<ICommandResult>(jsonResponse, jsonSerializerSettings);
                Console.WriteLine(
                    $"Received response from server, result is {JsonConvert.SerializeObject(deserializedCommandResult)}");
            }
        }

        private async Task<string> GenerateMessages()
        {
            var randomIndex = new Random().Next(0, 2);
            var msg = messages[randomIndex];
            var json = JsonConvert.SerializeObject(msg, jsonSerializerSettings);
            
            var commandResult = await ProcessMessageGeneric(json);
            return JsonConvert.SerializeObject(commandResult, jsonSerializerSettings);
        }

        private async Task<ICommandResult> ProcessMessageGeneric(string json)
        {
            var receivedMessage = JsonConvert.DeserializeObject<IMessage>(json, jsonSerializerSettings);

            using var scope = _serviceProvider.CreateScope();
            ICommandResult result = null;
            try
            {
                
                var processors = scope.ServiceProvider.GetServices<IMessageCommandProcessorWithResult>();
                var processor = processors.Single(p => p.CanProcess(receivedMessage));
                result = await processor.ProcessAsync(receivedMessage);
                
                Console.WriteLine($"Message {receivedMessage.GetType().Name} processed {result.GetType().Name}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return result;
        }
    }
}
