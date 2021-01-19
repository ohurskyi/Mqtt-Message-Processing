using FluentResults;
using MessageProcessorMediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MediatRGenericApproachTest
{

    public class BackgroundMessageListener : BackgroundService
    {
        private static readonly List<IMessage> messages = new List<IMessage>
        {
            new UpdateTimezoneMessage { Timezone = "Lviv" },
            new PairStartButtonMessage { ButtonId = "Button id", TcuId = "A"}
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

                GenerateMessages();
            }
        }

        private void GenerateMessages()
        {
            var randomIndex = new Random().Next(0, 2);
            var msg = messages[randomIndex];
            var json = JsonConvert.SerializeObject(msg, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Objects
            });
            ProcessMessageGeneric(json);
        }

        private async void ProcessMessageDynamic(string json)
        {
            var receivedMessage = JsonConvert.DeserializeObject<IMessage>(json, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Objects
            });

            using var scope = _serviceProvider.CreateScope();
            var processorType = typeof(IMessageProcessor<>).MakeGenericType(receivedMessage.GetType());

            var processsor = (dynamic)scope.ServiceProvider.GetService(processorType);

            try
            {
                Result result = await processsor.ProcessAsync((dynamic)receivedMessage);
                Console.WriteLine($"Message {receivedMessage.GetType()} processed {result}");
                Console.WriteLine($"Wait for next ------------------");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private async void ProcessMessageGeneric(string json)
        {
            var receivedMessage = JsonConvert.DeserializeObject<IMessage>(json, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Objects
            });

            using var scope = _serviceProvider.CreateScope();

            var processsors = scope.ServiceProvider.GetService<IEnumerable<IMessageProcessorTest>>();
            var processsor = processsors.Single(p => p.CanProcess(receivedMessage));

            try
            {
                Result result = await processsor.ProcessAsync(receivedMessage);
                Console.WriteLine($"Message {receivedMessage.GetType()} processed {result}");
                Console.WriteLine($"Wait for next ------------------");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
